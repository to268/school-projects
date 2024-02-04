using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class SousLocalisationManager : IDataRepository<SousLocalisation>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public SousLocalisationManager() { }
        public SousLocalisationManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<SousLocalisation>>> GetAllAsync()
        {
            return await clubMedDBContext.SousLocalisations
                    .Include("LesLocalisations")
                    .ToListAsync();
        }
        public async Task<ActionResult<SousLocalisation>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.SousLocalisations
                    .Include("LesLocalisations")
                    .FirstOrDefaultAsync(u => u.SousLocalisationId == id);
        }
        public async Task<ActionResult<SousLocalisation>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.SousLocalisations
                    .Include("LesLocalisations")
                    .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(SousLocalisation entity)
        {
            await clubMedDBContext.SousLocalisations.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(SousLocalisation sousLocalisation, SousLocalisation entity)
        {
            clubMedDBContext.Entry(sousLocalisation).State = EntityState.Modified;
            sousLocalisation.SousLocalisationId = entity.SousLocalisationId;
            sousLocalisation.Nom = entity.Nom;
            sousLocalisation.LesResorts = sousLocalisation.LesResorts;
            sousLocalisation.LesLocalisations = sousLocalisation.LesLocalisations;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(SousLocalisation sousLocalisation)
        {
            clubMedDBContext.SousLocalisations.Remove(sousLocalisation);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
