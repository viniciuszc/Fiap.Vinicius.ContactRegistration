using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Contacts.DeleteContacts;
using Application.UseCase.Contacts.GetContacts;
using Application.UseCase.Contacts.UpdateContact;
using Application.UseCase.Contacts.UpdateContacts;
using Application.UseCase.GetRegions;
using Application.UseCase.Users.CreateUser;
using Application.UseCase.Users.GetUserToken;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Application;

[ExcludeFromCodeCoverage]
public static class ApplicationExtension
{
    public static void AddUseCases(this IServiceCollection services)
    {        
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IGetTokenUseCase, GetTokenUseCase>();
        services.AddScoped<IGetContactsUseCase, GetContactsUseCase>();
        services.AddScoped<ICreateContactUseCase, CreateContactUseCase>();
        services.AddScoped<IUpdateContactUseCase, UpdateContactUseCase>();
        services.AddScoped<IDeleteContactUseCase, DeleteContactUseCase>();
        services.AddScoped<IGetRegionsUseCase, GetRegionsUseCase>();
        services.AddScoped<IValidator<CreateUserInput>,CreateUserValidator>();
        services.AddScoped<IValidator<CreateContactInput>, CreateContactValidator>();
        services.AddScoped<IValidator<UpdateContactInput>, UpdateContactValidator>();
    }
}