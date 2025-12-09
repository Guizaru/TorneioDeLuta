using System.Text.Json.Serialization;
using API_Torneio.Context;
using API_Torneio.Extensions;
using API_Torneio.Filters;
using API_Torneio.Logging;
using API_Torneio.Repositories;
using API_Torneio.Repositories.Interfaces;
using API_Torneio.Services.LutadorService;
using API_Torneio.Services.TorneioService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

builder.Services.AddScoped<ILutadorServiceInterface, LutadorService>();
builder.Services.AddScoped<IFasesTorneioService, FasesTorneioService>();
builder.Services.AddScoped<ITorneioServiceInterface, TorneioService>();
builder.Services.AddScoped<ILutadorRepository, LutadorRepository>();
builder.Services.AddScoped<ITorneioRepository, TorneioRepository>();
builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
