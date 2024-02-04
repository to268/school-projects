using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class PhotoManager : IDataRepository<Photo>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public PhotoManager() { }
        public PhotoManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Photo>>> GetAllAsync()
        {
            return await clubMedDBContext.Photos
                    .Include("LesResorts")
                    .ToListAsync();
        }
        public async Task<ActionResult<Photo>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Photos
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.PhotoId == id);
        }

        public async Task<ActionResult<Photo>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Photos
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.Lien.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Photo entity)
        {
            await clubMedDBContext.Photos.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Photo photo, Photo entity)
        {
            clubMedDBContext.Entry(photo).State = EntityState.Modified;
            photo.PhotoId = entity.PhotoId;
            photo.Lien = entity.Lien;
            photo.LesBars = photo.LesBars;
            photo.LesRestaurants = photo.LesRestaurants;
            photo.LesTypeActivites = photo.LesTypeActivites;
            photo.LesAvis = photo.LesAvis;
            photo.LesResorts = photo.LesResorts;
            photo.LesDomaineSkiables = photo.LesDomaineSkiables;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Photo entity)
        {
            clubMedDBContext.Photos.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
