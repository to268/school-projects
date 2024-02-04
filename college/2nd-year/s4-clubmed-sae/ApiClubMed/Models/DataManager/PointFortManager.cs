using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class PointFortManager : IDataRepository<PointFort>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public PointFortManager() { }
        public PointFortManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<PointFort>>> GetAllAsync()
        {
            return await clubMedDBContext.PointForts
                   .Include("LesTypeChambres")
                   .ToListAsync();
        }
        public async Task<ActionResult<PointFort>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.PointForts
                   .Include("LesTypeChambres")
                   .FirstOrDefaultAsync(u => u.PointFortId == id);
        }

        public async Task<ActionResult<PointFort>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.PointForts
                   .Include("LesTypeChambres")
                   .FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(PointFort entity)
        {
            await clubMedDBContext.PointForts.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(PointFort pointFort, PointFort entity)
        {
            clubMedDBContext.Entry(pointFort).State = EntityState.Modified;
            pointFort.PointFortId = entity.PointFortId;
            pointFort.Libelle = entity.Libelle;
            pointFort.LesTypeChambres = pointFort.LesTypeChambres;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(PointFort entity)
        {
            clubMedDBContext.PointForts.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
