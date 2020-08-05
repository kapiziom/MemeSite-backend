using FluentValidation;
using MemeSite.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using MemeSite.Domain.Validators;
using MemeSite.Domain.Interfaces;
using MemeSite.Application.Services;
using MemeSite.Domain.Models;
using MemeSite.Application.Interfaces;

namespace MemeSite.Api.Configuration
{
    public static class RepositoryServicesSetup
    {
        public static IServiceCollection AddRepositoryServicesSetup(
          this IServiceCollection services)
        {
            
            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            services.AddScoped<IMemeRepository, MemeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
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
