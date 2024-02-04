using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class BarManager : IDataRepository<Bar>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public BarManager() { }
        public BarManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Bar>>> GetAllAsync()
        {

            return await clubMedDBContext.Bars.ToListAsync();
        }
        public async Task<ActionResult<Bar>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Bars.FirstOrDefaultAsync(u => u.BarId == id);
        }

        public async Task<ActionResult<IEnumerable<Bar>>> GetByResortIdAsync(int id)
        {
            return await clubMedDBContext.Bars.Where(u => u.ResortId == id).ToListAsync();
        }

        public async Task<ActionResult<Bar>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Bars
                   .Include("LaPhoto")
                   .Include("LeResort")
                   .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }

        public async Task AddAsync(Bar entity)
        {
            await clubMedDBContext.Bars.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Bar bar, Bar entity)
        {
            clubMedDBContext.Entry(bar).State = EntityState.Modified;
            bar.BarId = entity.BarId;
            bar.Description = entity.Description;
            bar.LaPhoto = bar.LaPhoto;
            bar.LeResort = bar.LeResort;
            bar.Nom = entity.Nom;
            bar.PhotoId = entity.PhotoId;
            bar.ResortId = entity.ResortId;
            await clubMedDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Bar entity)
        {
            clubMedDBContext.Bars.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
