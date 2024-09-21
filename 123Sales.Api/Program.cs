using _123Sales.Api.Extensions;
using _123Sales.Application.Profiles;
using _123Sales.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurando o Serilog para usar o appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()   // Permite qualquer origem
            .AllowAnyMethod()   // Permite qualquer método (GET, POST, etc.)
            .AllowAnyHeader();  // Permite qualquer cabeçalho
    });
});

// Adicionando serviços da aplicação e o Swagger
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurando o DbContext com SQL Server e a string de conexão
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseSqlServer(connectionString, options =>
    options.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorNumbersToAdd: null)));

// Adicionando AutoMapper e registrando todos os Profiles
builder.Services.AddAutoMapper(typeof(SaleProfile).Assembly);

// Adiciona MediatR e registra todos os handlers do assembly correto
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(_123Sales.Application.Handlers.CreateSaleCommandHandler).Assembly));

try
{
    var app = builder.Build();

    // Aplicando as migrations automaticamente ao iniciar a aplicação
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
        dbContext.Database.Migrate();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }

    app.UseHttpsRedirection();
    // Aplicando a política de CORS
    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("Starting the web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}