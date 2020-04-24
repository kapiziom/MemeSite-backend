using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface IFavouriteService : IGenericService<Favourite>
    {
        Task<bool> InsertFavourite(AddFavouriteVM fav);
        Task<bool> DeleteFavourite(int memeId, string userId);
        Task<int> CountUsersFavourites(int memeId, string userId);

    }
}
