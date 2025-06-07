using MasterThesis.Core; // Core namespace for ISignatureSchemeSelector

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSingleton<ISignatureSchemeSelector, SignatureSchemeSelector>();

// Optional: add Swagger for local testing & easier documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map controller endpoints
app.MapControllers();

app.Run();
