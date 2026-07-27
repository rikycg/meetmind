using MeetMind.API.Middlewares;
using MeetMind.Application;
using MeetMind.Infrastructure;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MeetMind API",
        Version = "v1",
        Description = "AI-powered meeting assistant API"
    });
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetMind API v1");
        options.RoutePrefix = "docs";
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();