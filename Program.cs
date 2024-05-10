using HotelBackend;
using HotelBackend.Attributes;
using HotelBackend.Models;
using HotelBackend.Repositories;
using HotelBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
if (builder.Environment.IsDevelopment()) {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => {
        c.SchemaFilter<GeoLocationSchemaFilter>();    
        c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Name = "X-API-KEY",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Description = "Please provide the API Key"
        });

        c.OperationFilter<ApiKeyAuthOperationFilter>(); 
    });
}

builder.Services.Configure<SearchOptions>(builder.Configuration.GetSection("SearchSettings"));
builder.Services.AddScoped<IHotelScoreCalculationService, HotelScoreCalculationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

builder.Services.AddApiKeyAuth();
builder.Services.AddControllers();


var dbEnvironment = Environment.GetEnvironmentVariable("DATABASE_ENVIRONMENT");
var connectionStringName = dbEnvironment == "Testing" ? "TestDatabase" : "MainDatabase";
builder.Services.AddDbContext<MainDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString(connectionStringName),
    options => options.UseNetTopologySuite())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }