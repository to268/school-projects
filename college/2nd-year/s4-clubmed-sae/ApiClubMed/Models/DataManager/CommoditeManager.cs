using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class CommoditeManager : IDataRepository<Commodite>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public CommoditeManager() { }
        public CommoditeManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Commodite>>> GetAllAsync()
        {
            return await clubMedDBContext.Commodites
                   .Include("LesTypeChambres")
                   .ToListAsync();
        }
        public async Task<ActionResult<Commodite>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Commodites
                   .Include("LesTypeChambres")
                   .FirstOrDefaultAsync(u => u.CommoditeId == id);
        }

        public async Task<ActionResult<Commodite>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Commodites
                   .Include("LesTypeChambres")
                   .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Commodite entity)
        {
            await clubMedDBContext.Commodites.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Commodite commodite, Commodite entity)
        {
            clubMedDBContext.Entry(commodite).State = EntityState.Modified;
            commodite.CommoditeId = entity.CommoditeId;
            commodite.Nom = entity.Nom;
            commodite.LesTypeChambres = commodite.LesTypeChambres;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Commodite entity)
        {
            clubMedDBContext.Commodites.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
