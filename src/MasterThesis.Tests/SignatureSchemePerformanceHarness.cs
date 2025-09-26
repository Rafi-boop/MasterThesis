using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

internal static class SignatureSchemePerformanceHarness
{
    private const int WarmupIterations = 5;
    private const int TargetDurationMs = 1000; // ~1s per operation per scheme
    private static readonly byte[] Message = CreateMessage(1024);

    public static void Run(string[]? schemes = null, string? csvPath = null)
    {
        schemes ??= new[] { "falcon", "eddsa", "rsa", "ecdsa", "dilithium", "sphincs" };

        var sb = new StringBuilder();
        sb.AppendLine("Scheme,Operation,MeanMs,OpsPerSec,Iterations,MessageBytes,Timestamp");

        foreach (var name in schemes)
        {
            var dyn = GetScheme(name);

            // --- KeyGen ---
            var keygen = TimeLoop(TargetDurationMs, WarmupIterations, () => dyn.GenerateKeysDynamic());
            AppendCsv(sb, dyn.Name, "KeyGen", keygen.meanMs, keygen.opsPerSec, keygen.iters, Message.Length);

            // Use a fresh pair for signing/verification loops
            var keys = dyn.GenerateKeysDynamic();

            // --- Sign ---
            var sign = TimeLoop(TargetDurationMs, WarmupIterations, () => dyn.SignDynamic(Message, keys.PrivateKey));
            AppendCsv(sb, dyn.Name, "Sign", sign.meanMs, sign.opsPerSec, sign.iters, Message.Length);

            // Precompute one valid signature for verification loop
            var sig = dyn.SignDynamic(Message, keys.PrivateKey);

            // --- Verify ---
            var verify = TimeLoop(TargetDurationMs, WarmupIterations, () => dyn.VerifyDynamic(Message, sig, keys.PublicKey));
            AppendCsv(sb, dyn.Name, "Verify", verify.meanMs, verify.opsPerSec, verify.iters, Message.Length);
        }

        var outPath = csvPath ?? Path.Combine(AppContext.BaseDirectory, $"signature_perf_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv");
        File.WriteAllText(outPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine(sb.ToString());
        Console.WriteLine($"Wrote: {outPath}");
    }

    private static (double meanMs, double opsPerSec, int iters) TimeLoop(int targetMs, int warmup, Action action)
    {
        for (int i = 0; i < warmup; i++) action();

        var sw = Stopwatch.StartNew();
        int iters = 0;
        while (sw.ElapsedMilliseconds < targetMs)
        {
            action();
            iters++;
        }
        sw.Stop();

        var meanMs = iters > 0 ? sw.Elapsed.TotalMilliseconds / iters : double.NaN;
        var opsPerSec = meanMs > 0 ? 1000.0 / meanMs : 0.0;
        return (meanMs, opsPerSec, iters);
    }

    private static void AppendCsv(StringBuilder sb, string scheme, string op, double meanMs, double opsPerSec, int iters, int msgBytes)
    {
        sb.AppendLine(string.Join(",",
            Escape(scheme),
            Escape(op),
            meanMs.ToString("F4", CultureInfo.InvariantCulture),
            opsPerSec.ToString("F2", CultureInfo.InvariantCulture),
            iters.ToString(CultureInfo.InvariantCulture),
            msgBytes.ToString(CultureInfo.InvariantCulture),
            DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)));
    }

    private static string Escape(string s) => s.Contains(',') ? $"\"{s.Replace("\"", "\"\"")}\"" : s;

    private static byte[] CreateMessage(int size)
    {
        var m = new byte[size];
        for (int i = 0; i < m.Length; i++) m[i] = (byte)(i & 0xFF);
        return m;
    }

    private static ISignatureSchemeDynamic GetScheme(string schemeName)
    {
        // Same selection approach as your existing tests
        var selector = new SignatureSchemeSelector();
        return (ISignatureSchemeDynamic)selector.GetRawScheme(schemeName);
    }
}
