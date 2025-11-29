using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.SimpleMediator.Implementation;
using NerdStore.SimpleMediator.Interfaces;

namespace NerdStore.SimpleMediator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimpleMediator(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = [.. AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.FullName))];
        }

        services.AddSingleton<IMediator, Mediator>();

        RegisterHandlers(services, assemblies, typeof(IRequestHandler<,>));
        RegisterHandlers(services, assemblies, typeof(INotificationHandler<>));

        return services;
    }

    private static void RegisterHandlers(IServiceCollection services, Assembly[] assemblies, Type handlerInterface)
    {
        var types = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Implementation = t,
                Interfaces = t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            });

        foreach (var type in types)
        {
            foreach (var @interface in type.Interfaces)
            {
                services.AddTransient(@interface, type.Implementation);
            }
        }
    }
}
