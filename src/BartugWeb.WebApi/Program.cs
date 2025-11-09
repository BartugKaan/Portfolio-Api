using BartugWeb.ApplicationLayer;
using BartugWeb.PersistanceLayer;
using BartugWeb.WebApi.Extensions;
using BartugWeb.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.MapEndpointDefinitions();

app.Run();

