using BartugWeb.PersistanceLayer;
using BartugWeb.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceLayer(builder.Configuration);
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

app.Run();

