using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ActiviteIncluseManager : IDataRepository<ActiviteIncluse>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ActiviteIncluseManager() { }
        public ActiviteIncluseManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ActiviteIncluse>>> GetAllAsync()
        {
            return await clubMedDBContext.ActiviteIncluses
                    .Include("LesResorts")
                    .ToListAsync();
        }
        public async Task<ActionResult<ActiviteIncluse>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteIncluses
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.ActiviteIncluseId == id);
        }

        public async Task<ActionResult<IEnumerable<ActiviteIncluse>>> GetByTypeActiviteIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteIncluses.Where(u => u.TypeActiviteId == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<ActiviteIncluse>>> GetByAgeMinAsync(int AgeMin)
        {
            return await clubMedDBContext.ActiviteIncluses
                    .Include("LesResorts")
                    .Where(u => u.AgeMin == AgeMin).ToListAsync();
        }

        public async Task<ActionResult<ActiviteIncluse>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.ActiviteIncluses
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.Titre == nom);
        }
        public async Task AddAsync(ActiviteIncluse entity)
        {
            await clubMedDBContext.ActiviteIncluses.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ActiviteIncluse activiteIncluse, ActiviteIncluse entity)
        {
            clubMedDBContext.Entry(activiteIncluse).State = EntityState.Modified;
            activiteIncluse.ActiviteIncluseId = entity.ActiviteIncluseId;
            activiteIncluse.TypeActiviteId = entity.TypeActiviteId;
            activiteIncluse.Titre = entity.Titre;
            activiteIncluse.Duree = entity.Duree;
            activiteIncluse.Description = entity.Description;
            activiteIncluse.Frequence = entity.Frequence;
            activiteIncluse.AgeMin = entity.AgeMin;
            activiteIncluse.LesResorts = activiteIncluse.LesResorts;
            activiteIncluse.LeTypeActivite = activiteIncluse.LeTypeActivite;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(ActiviteIncluse entity)
        {
            clubMedDBContext.ActiviteIncluses.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
