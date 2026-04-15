using Amazon.SecretsManager;
using Serilog;
using Serilog.AWS.Logging.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add default AWS options
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

// Register Secrets Manager
builder.Services.AddAWSService<IAmazonSecretsManager>();

// logging with serilog and AWS CloudWatch
builder.Host.UseSerilog((_, loggerConfig) =>
{
    loggerConfig.WriteTo.Console().ReadFrom.Configuration(builder.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
