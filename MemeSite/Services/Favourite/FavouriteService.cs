using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class FavouriteService : GenericService<Favourite>, IFavouriteService
    {
        public FavouriteService(IGenericRepository<Favourite> _favRepository) : base(_favRepository) { }

        public async Task<bool> InsertFavourite(AddFavouriteVM fav)
        {
            if (await _repository.IsExistAsync(m => m.MemeRefId == fav.MemeId && m.UserId == fav.UserId) == false)
            {
                Favourite model = new Favourite()
                {
                    MemeRefId = fav.MemeId,
                    UserId = fav.UserId,
                };
                await _repository.InsertAsync(model);
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteFavourite(int memeId, string userId)
        {
            var model = await _repository.FindAsync(m => m.MemeRefId == memeId && m.UserId == userId);
            if (model != null)
            {
                await _repository.DeleteAsync(model);
                return true;
            }
            else return false;
        }

        public async Task<int> CountUsersFavourites(int memeId, string userId)
            => await _repository.CountAsync(m => m.MemeRefId == memeId && m.UserId == userId);
    }
}
