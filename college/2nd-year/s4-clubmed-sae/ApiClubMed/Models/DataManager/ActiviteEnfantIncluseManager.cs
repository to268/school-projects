using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ActiviteEnfantIncluseManager : IDataRepository<ActiviteEnfantIncluse>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ActiviteEnfantIncluseManager() { }
        public ActiviteEnfantIncluseManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ActiviteEnfantIncluse>>> GetAllAsync()
        {
            return await clubMedDBContext.ActiviteEnfantIncluses
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .ToListAsync();
        }
        public async Task<ActionResult<ActiviteEnfantIncluse>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteEnfantIncluses
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .FirstOrDefaultAsync(u => u.ActiviteEnfantIncluseId == id);
        }
        public async Task<ActionResult<ActiviteEnfantIncluse>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.ActiviteEnfantIncluses
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .FirstOrDefaultAsync(u => u.Titre.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(ActiviteEnfantIncluse entity)
        {
            await clubMedDBContext.ActiviteEnfantIncluses.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ActiviteEnfantIncluse activiteEnfantIncluse, ActiviteEnfantIncluse entity)
        {
            clubMedDBContext.Entry(activiteEnfantIncluse).State = EntityState.Modified;
            activiteEnfantIncluse.ActiviteEnfantIncluseId = entity.ActiviteEnfantIncluseId;
            activiteEnfantIncluse.Titre = entity.Titre;
            activiteEnfantIncluse.Duree = entity.Duree;
            activiteEnfantIncluse.Description = entity.Description;
            activiteEnfantIncluse.Frequence = entity.Frequence;
            activiteEnfantIncluse.LesResorts = activiteEnfantIncluse.LesResorts;
            activiteEnfantIncluse.LesTrancheAges = activiteEnfantIncluse.LesTrancheAges;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(ActiviteEnfantIncluse activiteEnfantIncluse)
        {
            clubMedDBContext.ActiviteEnfantIncluses.Remove(activiteEnfantIncluse);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
