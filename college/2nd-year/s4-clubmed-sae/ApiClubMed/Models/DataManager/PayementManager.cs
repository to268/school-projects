using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class PayementManager : IDataRepository<Payement>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public PayementManager() { }
        public PayementManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Payement>>> GetAllAsync()
        {
            return await clubMedDBContext.Payements.ToListAsync();
        }
        public async Task<ActionResult<Payement>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Payements.FirstOrDefaultAsync(u => u.PayementId == id);
        }

        public async Task<ActionResult<IEnumerable<Payement>>> GetByReservationIdAsync(int id)
        {
            return await clubMedDBContext.Payements.Where(u => u.ReservationId == id).ToListAsync();
        }

        public async Task<ActionResult<Payement>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        public async Task AddAsync(Payement entity)
        {
            await clubMedDBContext.Payements.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Payement payement, Payement entity)
        {
            clubMedDBContext.Entry(payement).State = EntityState.Modified;
            payement.PayementId = entity.PayementId;
            payement.ReservationId = entity.ReservationId;
            payement.Montant = entity.Montant;
            payement.LaReservation = payement.LaReservation;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Payement entity)
        {
            clubMedDBContext.Payements.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
