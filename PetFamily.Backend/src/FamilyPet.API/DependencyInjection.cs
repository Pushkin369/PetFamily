using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.CreateVolunteer;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PetFamily.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });
        return services;
    }
}