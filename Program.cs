using Microsoft.EntityFrameworkCore;
using practice.Middlewares;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Db;
using TestingPlatform.Infrastructure.Repositories;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps("TestingPlatform.Infrastructure"));
builder.Services.AddAutoMapper(cfg => cfg.AddMaps("TestingPlatform"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TestingPlatform.Infrastructure")));
Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Information()
   .WriteTo.Console()
   .WriteTo.File("logs/app.log")
   .CreateLogger();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Information()
   .WriteTo.Console()
   .WriteTo.File(
       formatter: new JsonFormatter(),
       path: "logs/structured-.json")
   .WriteTo.SQLite("logs/logs.db")
   .CreateLogger();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


