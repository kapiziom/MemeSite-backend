using MemeSite.Domain;
using MemeSite.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemeSite.Api.Services
{
    public interface IFavouriteService : IGenericService<Favourite>
    {
        Task<bool> InsertFavourite(AddFavouriteVM fav);
        Task DeleteFavourite(int memeId, string userId);
        Task<List<Meme>> GetUsersFavourites(string userId);
        Task<int> CountUsersFavourites(int memeId, string userId);

    }
}
