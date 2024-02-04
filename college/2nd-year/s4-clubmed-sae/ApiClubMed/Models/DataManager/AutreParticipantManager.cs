using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class AutreParticipantManager : IDataRepository<AutreParticipant>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public AutreParticipantManager() { }
        public AutreParticipantManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<AutreParticipant>>> GetAllAsync()
        {
            return await clubMedDBContext.AutreParticipants
                    .Include("LesReservations")
                    .ToListAsync();
        }
        public async Task<ActionResult<AutreParticipant>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.AutreParticipants
                    .Include("LesReservations")
                    .FirstOrDefaultAsync(u => u.AutreParticipantId == id);
        }

        public async Task<ActionResult<AutreParticipant>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.AutreParticipants
                    .Include("LesReservations")
                    .FirstOrDefaultAsync(u => u.Nom == nom);
        }
        public async Task AddAsync(AutreParticipant entity)
        {
            await clubMedDBContext.AutreParticipants.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(AutreParticipant autreParticipant, AutreParticipant entity)
        {
            clubMedDBContext.Entry(autreParticipant).State = EntityState.Modified;
            autreParticipant.AutreParticipantId = entity.AutreParticipantId;
            autreParticipant.CiviliteId = entity.CiviliteId;
            autreParticipant.Prenom = entity.Prenom;
            autreParticipant.Nom = entity.Nom;
            autreParticipant.DateNaissance = entity.DateNaissance;
            autreParticipant.LaCivilite = autreParticipant.LaCivilite;
            autreParticipant.LesReservations = autreParticipant.LesReservations;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(AutreParticipant entity)
        {
            clubMedDBContext.AutreParticipants.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
