using MiddlewareExample.CustomMiddleware;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHello();
app.UseMiddleware<StatsLoggerMiddleware>();

app.Use(async (context, NextMiddleware) =>
{
    //1 - Operate on the request
    if (context.Request.Headers.ContainsKey("Debug"))
    {
        Console.WriteLine($"Got request. Method={context.Request.Method} Path={context.Request.Path}");

        var sw = Stopwatch.StartNew();

        //2 - Call the next middleware
        await NextMiddleware();

        //3 - Operate on the response
        sw.Stop();
        Console.WriteLine($"Request finished. Method={context.Request.Method} Path={context.Request.Path} StatusCode={context.Response.StatusCode} ElapsedMilliseconds={sw.ElapsedMilliseconds}");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
