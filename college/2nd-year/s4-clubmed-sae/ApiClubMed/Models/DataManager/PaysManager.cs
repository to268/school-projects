using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class PaysManager : IDataRepository<Pays>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public PaysManager() { }
        public PaysManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Pays>>> GetAllAsync()
        {
            return await clubMedDBContext.Pays.ToListAsync();
        }
        public async Task<ActionResult<Pays>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Pays.FirstOrDefaultAsync(u => u.PaysId == id);
        }
        public async Task<ActionResult<Pays>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Pays.FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Pays entity)
        {
            await clubMedDBContext.Pays.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Pays pays, Pays entity)
        {
            clubMedDBContext.Entry(pays).State = EntityState.Modified;
            pays.PaysId = entity.PaysId;
            pays.Nom = entity.Nom;
            pays.LesClients = pays.LesClients;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Pays entity)
        {
            clubMedDBContext.Pays.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
