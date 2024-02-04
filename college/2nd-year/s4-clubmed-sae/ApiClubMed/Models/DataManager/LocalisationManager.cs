using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class LocalisationManager : IDataRepository<Localisation>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public LocalisationManager() { }
        public LocalisationManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Localisation>>> GetAllAsync()
        {
            return await clubMedDBContext.Localisations
                    .Include("LesSousLocalisations")
                    .ToListAsync();
        }
        public async Task<ActionResult<Localisation>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Localisations
                    .Include("LesSousLocalisations")
                    .FirstOrDefaultAsync(u => u.LocalisationId == id);
        }
        public async Task<ActionResult<Localisation>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Localisations
                    .Include("LesSousLocalisations")
                    .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Localisation entity)
        {
            await clubMedDBContext.Localisations.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Localisation localisation, Localisation entity)
        {
            clubMedDBContext.Entry(localisation).State = EntityState.Modified;
            localisation.LocalisationId = entity.LocalisationId;
            localisation.Nom = entity.Nom;
            localisation.LesResorts = localisation.LesResorts;
            localisation.LesSousLocalisations = localisation.LesSousLocalisations;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Localisation localisation)
        {
            clubMedDBContext.Localisations.Remove(localisation);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
