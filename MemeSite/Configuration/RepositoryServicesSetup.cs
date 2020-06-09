using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Validators;
using MemeSite.Data.Repository;
using MemeSite.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Configuration
{
    public static class RepositoryServicesSetup
    {
        public static IServiceCollection AddRepositoryServicesSetup(
          this IServiceCollection services)
        {
            //repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IMemeService, MemeService>();
            services.AddScoped<IUserService, UserService>();
            //validators
            services.AddScoped<IValidator<Category>, CategoryValidator>();
            services.AddScoped<IValidator<Comment>, CommentValidator>();
            services.AddScoped<IValidator<Favourite>, FavouriteValidator>();
            services.AddScoped<IValidator<Meme>, MemeValidator>();
            services.AddScoped<IValidator<Vote>, VoteValidator>();

            return services;
        }
    }
}
