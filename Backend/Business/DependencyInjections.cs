using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Business;
public static class DependencyInjections
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(DependencyInjections).Assembly);

        return services;
    }
}
