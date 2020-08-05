using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeSite.Application.Interfaces
{
    public interface IFavouriteService : IGenericService<Favourite>
    {
        Task<bool> InsertFavourite(AddFavouriteVM fav);
        Task DeleteFavourite(int memeId, string userId);
        Task<List<Meme>> GetUsersFavourites(string userId);
        Task<int> CountUsersFavourites(int memeId, string userId);

    }
}
