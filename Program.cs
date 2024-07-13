using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;
using PaparaPatika.IServices;
using PaparaPatika.Middlewares;
using PaparaPatika.Repositories;
using PaparaPatika.Services;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var columnOptions = new ColumnOptions();
Logger log = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/log.txt").WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("PaparaDB"), new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        columnOptions: columnOptions).Enrich.FromLogContext().MinimumLevel.Information().CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PaparaDB");
builder.Services.AddDbContext<PaparaDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseMiddleware<LogMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
