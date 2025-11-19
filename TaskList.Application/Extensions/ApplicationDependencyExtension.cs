using Microsoft.Extensions.DependencyInjection;
using TaskLists.Application.Interfaces;
using TaskLists.Application.Services;

namespace TaskLists.Application.Extensions;

public static class ApplicationDependencyExtension
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITaskListService, TaskListService>();
        return services;
    }
}
