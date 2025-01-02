using API.Endpoints;
using API.Exception;
using API.Extension;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterServices(builder.Configuration); //Extension Method

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", x =>
    {
        x.WithOrigins("http://localhost:4200");
        x.AllowAnyHeader();
        x.AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Default");
app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapGroup("/api/")
   .WithTags("Product Endpoints")
   .MapProductEndPoint();

app.Run();
