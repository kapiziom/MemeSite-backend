using AutoMapper;
using FluentValidation;
using MemeSite.Data.Repository;
using MemeSite.Domain;
using MemeSite.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemeSite.Api.Services
{
    public class FavouriteService : GenericService<Favourite>, IFavouriteService
    {
        public FavouriteService(
            IGenericRepository<Favourite> _favRepository,
            IValidator<Favourite> validator) : base(_favRepository, validator) { }

        public async Task<bool> InsertFavourite(AddFavouriteVM fav)
        {
            if (await _repository.IsExistAsync(m => m.MemeRefId == fav.MemeId && m.UserId == fav.UserId) == false)
            {
                Favourite entity = new Favourite()
                {
                    MemeRefId = fav.MemeId,
                    UserId = fav.UserId,
                    CreateFavDate = DateTime.Now
                };
                await _repository.InsertAsync(entity);
                return true;
            }
            else return false;
        }

        public async Task DeleteFavourite(int memeId, string userId)
        {
            await _repository.DeleteAsync(memeId, userId);
        }

        public async Task<List<Meme>> GetUsersFavourites(string userId)
        {
            var query = _repository.Query()
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreateFavDate)
                .Include(x => x.Meme).ThenInclude(y => y.Comments)
                .Include(x => x.Meme).ThenInclude(y => y.PageUser)
                .Include(x => x.Meme).ThenInclude(y => y.Votes)
                .Include(x => x.Meme).ThenInclude(y => y.Favourites)
                .Include(x => x.Meme).ThenInclude(y => y.Category);

            var memelist = from q in query select q.Meme;

            var result = await memelist.ToListAsync();

            return result;
        }

        public async Task<int> CountUsersFavourites(int memeId, string userId)
            => await _repository.CountAsync(m => m.MemeRefId == memeId && m.UserId == userId);
    }
}
