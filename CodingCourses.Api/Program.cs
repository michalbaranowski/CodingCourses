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
builder.Services.AddCors(options => options.AddPolicy(name: "CodingCoursesOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
        policy.WithOrigins("http://codingcourses.mbaranowski.it").AllowAnyMethod().AllowAnyHeader();
    }));

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

app.UseCors("CodingCoursesOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
