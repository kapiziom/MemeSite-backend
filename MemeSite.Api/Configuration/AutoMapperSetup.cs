using AutoMapper;
using MemeSite.Api.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MemeSite.Api.Configuration
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddAutoMapperSetup(
          this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToViewModelMappingProfile());
                mc.AddProfile(new ViewModelToModelMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
