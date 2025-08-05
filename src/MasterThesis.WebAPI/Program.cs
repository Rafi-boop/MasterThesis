using MasterThesis.Core; // Core namespace for ISignatureSchemeSelector

/// <summary>
/// Entry point for the MasterThesis Web API application.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSingleton<ISignatureSchemeSelector, SignatureSchemeSelector>();

// Optional: add Swagger for local testing and API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map controller endpoints
app.MapControllers();

// Run the application
app.Run();
