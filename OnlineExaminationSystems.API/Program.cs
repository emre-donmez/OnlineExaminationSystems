using FluentValidation;
using OnlineExaminationSystems.API.Data.Context;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Helpers;
using OnlineExaminationSystems.API.Model.Repository;
using OnlineExaminationSystems.API.Services;
using OnlineExaminationSystems.API.Services.Abstract;
using OnlineExaminationSystems.API.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IValidator<UserUpdateRequestModel>, UserUpdateRequestModelValidator>();
builder.Services.AddScoped<IPasswordHashHelper, PasswordHashHelper>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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