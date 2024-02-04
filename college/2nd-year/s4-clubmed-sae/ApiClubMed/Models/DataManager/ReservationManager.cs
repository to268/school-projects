using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ReservationManager : IDataRepository<Reservation>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ReservationManager() { }
        public ReservationManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllAsync()
        {
            return await clubMedDBContext.Reservations
                    .Include("LesAutreParticipants")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteEnfantCartes")
                    .ToListAsync();
        }
        public async Task<ActionResult<Reservation>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Reservations
                    .Include("LesAutreParticipants")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteEnfantCartes")
                    .FirstOrDefaultAsync(u => u.ReservationId == id);
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> GetByClientIdAsync(int id)
        {
            return await clubMedDBContext.Reservations
                    .Include("LesAutreParticipants")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteEnfantCartes")
                    .Where(u => u.ClientId == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> GetByResortIdAsync(int id)
        {
            return await clubMedDBContext.Reservations
                    .Include("LesAutreParticipants")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteEnfantCartes")
                    .Where(u => u.ResortId == id).ToListAsync();
        }

        public async Task<ActionResult<Reservation>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        public async Task AddAsync(Reservation entity)
        {
            await clubMedDBContext.Reservations.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Reservation client, Reservation entity)
        {
            clubMedDBContext.Entry(client).State = EntityState.Modified;
            client.ReservationId = entity.ReservationId;
            client.ClientId = entity.ClientId;
            client.TransportId = entity.TransportId;
            client.ResortId = entity.ResortId;
            client.DateDebut = entity.DateDebut;
            client.DateFin = entity.DateFin;
            client.Prix = entity.Prix;
            client.Confirmation = entity.Confirmation;
            client.Validation = entity.Validation;
            client.LeResort = client.LeResort;
            client.LesActiviteCartes = client.LesActiviteCartes;
            client.LesPayements = client.LesPayements;
            client.LesActiviteEnfantCartes = client.LesActiviteEnfantCartes;
            client.LesActiviteCartes = client.LesActiviteCartes;
            client.LeClient = client.LeClient;
            client.LeTransport = client.LeTransport;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Reservation entity)
        {
            clubMedDBContext.Reservations.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
