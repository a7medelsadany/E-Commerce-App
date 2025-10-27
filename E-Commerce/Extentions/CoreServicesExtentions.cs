using Services;
using Services.Abstractions;

namespace E_Commerce.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;
        }
    }
}
