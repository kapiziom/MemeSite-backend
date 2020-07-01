using FluentValidation;
using MemeSite.Data.Repository;
using MemeSite.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using MemeSite.Domain;
using MemeSite.Domain.Validators;

namespace MemeSite.Api.Configuration
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
