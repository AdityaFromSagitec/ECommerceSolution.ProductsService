using System.Text.Json.Serialization;
using ECommerce.ProductsMicroService.API.APIEndpoints;
using ECommerce.ProductsService.BussinessLogicLayer;
using ECommerce.ProductsService.DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBussinessLogicLayer();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

// To avoid cyclical reference issue while serializing the object to json
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        //angular app runs on port 4200
        builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapProductAPIEndpoints();   
app.Run();
