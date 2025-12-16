var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Health endpoint (for Kubernetes)
app.MapGet("/ping", () => Results.Ok("pong"));
app.MapGet("/health/live", () => Results.Ok());
app.MapGet("/health/ready", () => Results.Ok());
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
