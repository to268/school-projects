using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class RestaurantManager : IDataRepository<Restaurant>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public RestaurantManager() { }
        public RestaurantManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllAsync()
        {
            return await clubMedDBContext.Restaurants.ToListAsync();
        }
        public async Task<ActionResult<Restaurant>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Restaurants.FirstOrDefaultAsync(u => u.RestaurantId == id);
        }

        public async Task<ActionResult<IEnumerable<Restaurant>>> GetByResortIdAsync(int id)
        {
            return await clubMedDBContext.Restaurants.Where(u => u.ResortId == id).ToListAsync();
        }

        public async Task<ActionResult<Restaurant>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Restaurants
                   .Include("LaPhoto")
                   .Include("LeResort")
                   .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Restaurant entity)
        {
            await clubMedDBContext.Restaurants.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Restaurant restaurant, Restaurant entity)
        {
            clubMedDBContext.Entry(restaurant).State = EntityState.Modified;
            restaurant.RestaurantId = entity.RestaurantId;
            restaurant.Description = entity.Description;
            restaurant.LaPhoto = restaurant.LaPhoto;
            restaurant.LeResort = restaurant.LeResort;
            restaurant.Nom = entity.Nom;
            restaurant.PhotoId = entity.PhotoId;
            restaurant.ResortId = entity.ResortId;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Restaurant entity)
        {
            clubMedDBContext.Restaurants.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
