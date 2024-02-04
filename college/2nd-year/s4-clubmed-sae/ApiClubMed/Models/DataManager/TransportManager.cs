using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class TransportManager : IDataRepository<Transport>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public TransportManager() { }
        public TransportManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Transport>>> GetAllAsync()
        {
            return await clubMedDBContext.Transports.ToListAsync();
        }
        public async Task<ActionResult<Transport>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Transports.FirstOrDefaultAsync(u => u.TransportId == id);
        }
        public async Task<ActionResult<Transport>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Transports.FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Transport entity)
        {
            await clubMedDBContext.Transports.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Transport typeClub, Transport entity)
        {
            clubMedDBContext.Entry(typeClub).State = EntityState.Modified;
            typeClub.TransportId = entity.TransportId;
            typeClub.Libelle = entity.Libelle;
            typeClub.LesReservations = typeClub.LesReservations;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Transport typeClub)
        {
            clubMedDBContext.Transports.Remove(typeClub);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
