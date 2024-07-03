using AutoFilterer.Swagger;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Mappings;
using FlexiSourceIT.FlexMarathon.Application.Services;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Other services
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddTransient<IUserProfileService, UserProfileService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.UseAutoFiltererParameters();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
