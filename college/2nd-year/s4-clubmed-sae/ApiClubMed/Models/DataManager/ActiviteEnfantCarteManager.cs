using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ActiviteEnfantCarteManager : IDataRepository<ActiviteEnfantCarte>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ActiviteEnfantCarteManager() { }
        public ActiviteEnfantCarteManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ActiviteEnfantCarte>>> GetAllAsync()
        {
            return await clubMedDBContext.ActiviteEnfantCartes
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .Include("LesReservations")
                    .ToListAsync();
        }
        public async Task<ActionResult<ActiviteEnfantCarte>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteEnfantCartes
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .Include("LesReservations")
                    .FirstOrDefaultAsync(u => u.ActiviteEnfantCarteId == id);
        }
        public async Task<ActionResult<ActiviteEnfantCarte>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.ActiviteEnfantCartes
                    .Include("LesResorts")
                    .Include("LesTrancheAges")
                    .Include("LesReservations")
                    .FirstOrDefaultAsync(u => u.Titre.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(ActiviteEnfantCarte entity)
        {
            await clubMedDBContext.ActiviteEnfantCartes.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ActiviteEnfantCarte activiteEnfantCarte, ActiviteEnfantCarte entity)
        {
            clubMedDBContext.Entry(activiteEnfantCarte).State = EntityState.Modified;
            activiteEnfantCarte.ActiviteEnfantCarteId = entity.ActiviteEnfantCarteId;
            activiteEnfantCarte.Titre = entity.Titre;
            activiteEnfantCarte.Duree = entity.Duree;
            activiteEnfantCarte.Description = entity.Description;
            activiteEnfantCarte.Frequence = entity.Frequence;
            activiteEnfantCarte.Prix = entity.Prix;
            activiteEnfantCarte.LesResorts = activiteEnfantCarte.LesResorts;
            activiteEnfantCarte.LesTrancheAges = activiteEnfantCarte.LesTrancheAges;
            activiteEnfantCarte.LesReservations = activiteEnfantCarte.LesReservations;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(ActiviteEnfantCarte activiteEnfantCarte)
        {
            clubMedDBContext.ActiviteEnfantCartes.Remove(activiteEnfantCarte);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
