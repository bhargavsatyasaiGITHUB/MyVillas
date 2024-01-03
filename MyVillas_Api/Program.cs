using Microsoft.EntityFrameworkCore;
using MyVillas_Api;
using MyVillas_Api.Data;
using MyVillas_Api.Logging;
using MyVillas_Api.Repository;
using MyVillas_Api.Repository.IRepository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/villalogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();


//builder.Host.UseSerilog();
// Add services to the container.

//builder.Services.AddControllers(option =>
//{
//    option.ReturnHttpNotAcceptable=true;
//}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers(
).AddNewtonsoftJson();
builder.Services.AddScoped<IVillaRepository,VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
//builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
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
