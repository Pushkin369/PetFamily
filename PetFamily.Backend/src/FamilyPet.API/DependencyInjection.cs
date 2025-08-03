using PetFamily.Application.Volunteers.CreateVolunteer;

namespace PetFamily.Application;

public static class DependencyInjection
{
     public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}