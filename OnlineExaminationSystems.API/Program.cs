using FluentValidation;
using OnlineExaminationSystems.API.Model.Context;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddScoped<IValidator<UserUpdateRequestModel>, UserUpdateRequestModelValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();