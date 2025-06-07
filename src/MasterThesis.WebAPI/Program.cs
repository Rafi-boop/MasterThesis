var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register your selector (singleton for demo)
builder.Services.AddSingleton<ISignatureSchemeSelector, SignatureSchemeSelector>();

var app = builder.Build();

app.MapControllers(); // Map [ApiController] endpoints

app.Run();
