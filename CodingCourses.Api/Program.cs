using CodingCourses.DataAccess.Contracts.Contexts;
using CodingCourses.Domain.Contracts.Services;
using CodingCourses.Domain.Logic.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("CodingCoursesConnection");
builder.Services.AddDbContext<CodingCoursesDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICodingCoursesService, CodingCoursesService>();

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
