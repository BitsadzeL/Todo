//default
using Microsoft.EntityFrameworkCore;
using todoapi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TasksDBContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


//to solve CROS Problem
app.Use(async (context, next) =>
{
    // Handle preflight requests
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
    }
    else
    {
        // Allow requests from 'http://localhost:4200'
        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
        await next();
    }
});



app.MapControllers();

app.Run();