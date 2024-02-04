using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class CiviliteManager : IDataRepository<Civilite>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public CiviliteManager() { }
        public CiviliteManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Civilite>>> GetAllAsync()
        {
            return await clubMedDBContext.Civilites.ToListAsync();
        }
        public async Task<ActionResult<Civilite>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Civilites.FirstOrDefaultAsync(u => u.CiviliteId == id);
        }

        public async Task<ActionResult<Civilite>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Civilites.FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Civilite entity)
        {
            await clubMedDBContext.Civilites.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Civilite civilite, Civilite entity)
        {
            clubMedDBContext.Entry(civilite).State = EntityState.Modified;
            civilite.CiviliteId = entity.CiviliteId;
            civilite.Libelle = entity.Libelle;
            civilite.LesClients = civilite.LesClients;
            civilite.LesAutreParticipants = civilite.LesAutreParticipants;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Civilite entity)
        {
            clubMedDBContext.Civilites.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
