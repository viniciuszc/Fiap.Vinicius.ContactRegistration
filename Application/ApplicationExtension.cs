using Application.UseCase.GetContacts;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Application;

[ExcludeFromCodeCoverage]
public static class ApplicationExtension
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetContactsUseCase, GetContactsUseCase>();
    }
}