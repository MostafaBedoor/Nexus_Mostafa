using Nexus_Mostafa.Server;
using Nexus_Mostafa.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowAnyOrigin();
}));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(typeof(UserController));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyPolicy");
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
