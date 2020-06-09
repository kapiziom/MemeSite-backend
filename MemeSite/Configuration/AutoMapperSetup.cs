using AutoMapper;
using MemeSite.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Configuration
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
