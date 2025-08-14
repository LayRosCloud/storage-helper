using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Reflection;
using FluentMigrator.Runner;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Data.Migrations;
using StorageHandler.Utils.Middleware;

const string xmlFormat = "xml";
const string policyCors = "DefaultOrigins";
const string connectionString = "Connect";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var services = builder.Services;
services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString));
});
services.AddScoped<IStorageContext, DatabaseContext>();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddScoped<IUnitRepository, UnitRepository>();
services.AddScoped<ITransactionWrapper, TransactionWrapper>();
services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.AddOpenBehavior(typeof(ValidatorBehaviour<,>));
});
services.AddAutoMapper(config =>
{
    config.AddProfile<UnitMapper>();
    config.AddProfile<EntranceMapper>();
    config.AddProfile<EntranceBucketMapper>();
    config.AddProfile<ResourceMapper>();
});
services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.{xmlFormat}";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

services.AddCors(options =>
{
    options.AddPolicy(policyCors, new CorsPolicyBuilder()
        .WithHeaders("Authorization", "Content-Type", "Accept")
        .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
        .AllowAnyOrigin()
        .Build());
});

services.AddFluentMigratorCore()
    .ConfigureRunner(x => x.AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(InitialSchema).Assembly).For.Migrations()
    );
services.AddLogging(lb =>
{
    lb.AddFluentMigratorConsole();
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    if (runner.HasMigrationsToApplyUp())
    {
        runner.MigrateUp();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyCors);
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
