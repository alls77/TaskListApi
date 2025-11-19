using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TaskLists.Application.Interfaces;
using TaskLists.Infrastructure.Options;
using TaskLists.Infrastructure.Repositories;

namespace TaskLists.Infrastructure.Extensions;

public static class InfrastructureDependencyExtension
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<TaskListsDbOptions>()
            .Bind(config.GetSection(TaskListsDbOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var options = config
            .GetSection(TaskListsDbOptions.SectionName)
            .Get<TaskListsDbOptions>();

        if (options is null || string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("TaskListsDbOptions options are missing or incomplete in configuration.");

        services.AddSingleton<IMongoClient>(sp => new MongoClient(options.ConnectionString!));
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName!);
        });

        services.AddSingleton<MongoDbContext>();
        services.AddScoped<ITaskListRepository, TaskListRepository>();
        return services;
    }
}
