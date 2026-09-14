using DotNetEnv;
using TodoList_NET.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoList_NET.Extensions;

public static class ServiceCollectionDatabase
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        // Cargar las variables de entorno desde el archivo .env
        Env.Load();

        // Obtener los valores de las variables de entorno
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");

        // Construir la cadena de conexión
        var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

        // Configurar el contexto de la base de datos con PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}