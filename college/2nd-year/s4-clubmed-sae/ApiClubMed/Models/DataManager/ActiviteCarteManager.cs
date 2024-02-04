using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ActiviteCarteManager : IDataRepository<ActiviteCarte>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ActiviteCarteManager() { }
        public ActiviteCarteManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ActiviteCarte>>> GetAllAsync()
        {
            return await clubMedDBContext.ActiviteCartes
                    .Include("LesReservations")
                    .Include("LesResorts")
                    .ToListAsync();
        }
        public async Task<ActionResult<ActiviteCarte>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteCartes
                    .Include("LesReservations")
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.ActiviteCarteId == id);
        }

        public async Task<ActionResult<IEnumerable<ActiviteCarte>>> GetByTypeActiviteIdAsync(int id)
        {
            return await clubMedDBContext.ActiviteCartes
                    .Include("LesReservations")
                    .Include("LesResorts")
                    .Where(u => u.TypeActiviteId == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<ActiviteCarte>>> GetByAgeMinAsync(int AgeMin)
        {
            return await clubMedDBContext.ActiviteCartes
                    .Include("LesReservations")
                    .Include("LesResorts")
                    .Where(u => u.AgeMin == AgeMin).ToListAsync();
        }

        public async Task<ActionResult<ActiviteCarte>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.ActiviteCartes
                    .Include("LesReservations")
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.Titre == nom);
        }
        public async Task AddAsync(ActiviteCarte entity)
        {
            await clubMedDBContext.ActiviteCartes.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ActiviteCarte activiteCarte, ActiviteCarte entity)
        {
            clubMedDBContext.Entry(activiteCarte).State = EntityState.Modified;
            activiteCarte.ActiviteCarteId = entity.ActiviteCarteId;
            activiteCarte.TypeActiviteId = entity.TypeActiviteId;
            activiteCarte.Titre = entity.Titre;
            activiteCarte.Duree = entity.Duree;
            activiteCarte.Description = entity.Description;
            activiteCarte.Frequence = entity.Frequence;
            activiteCarte.AgeMin = entity.AgeMin;
            activiteCarte.Prix = entity.Prix;
            activiteCarte.LesReservations = activiteCarte.LesReservations;
            activiteCarte.LesResorts = activiteCarte.LesResorts;
            activiteCarte.LeTypeActivite = activiteCarte.LeTypeActivite;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(ActiviteCarte entity)
        {
            clubMedDBContext.ActiviteCartes.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
