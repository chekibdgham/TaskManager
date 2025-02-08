using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.Middelware;
using TaskManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with in-memory database
builder.Services.AddDbContext<TaskManagementDB>(options =>
    options.UseInMemoryDatabase("TaskManagementDB"));

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TaskToDoService>();


// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskManagementDB>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RoleBasedAccessControlMiddleware>();

app.MapControllers();

app.Run();
