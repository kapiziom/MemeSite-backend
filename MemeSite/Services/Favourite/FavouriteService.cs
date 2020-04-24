using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemeSite.Services
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
                    UserId = fav.UserId
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
            var favs = await GetAllFilteredIncludeAsync(m => m.UserId == userId,
                x => x.Meme.Comments,
                x => x.Meme.Favourites,
                x => x.Meme.Votes,
                x => x.Meme.PageUser,
                x => x.Meme.Category);
            List<Meme> list = new List<Meme>();
            foreach (var fav in favs)
            {
                list.Add(fav.Meme);
            }
            return list;
        }

        public async Task<int> CountUsersFavourites(int memeId, string userId)
            => await _repository.CountAsync(m => m.MemeRefId == memeId && m.UserId == userId);
    }
}
