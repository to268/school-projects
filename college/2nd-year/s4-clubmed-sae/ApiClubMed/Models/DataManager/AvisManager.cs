using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class AvisManager : IDataRepository<Avis>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public AvisManager() { }
        public AvisManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Avis>>> GetAllAsync()
        {
            return await clubMedDBContext.Avis.ToListAsync();
        }
        public async Task<ActionResult<Avis>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Avis.FirstOrDefaultAsync(u => u.AvisId == id);
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetByResortIdAsync(int id)
        {
            return await clubMedDBContext.Avis.Where(u => u.ResortId == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetByClientIdAsync(int id)
        {
            return await clubMedDBContext.Avis.Where(u => u.ClientId == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetByNoteAsync(int note)
        {
            return await clubMedDBContext.Avis.Where(u => u.Note == note).ToListAsync();
        }

        public async Task<ActionResult<Avis>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        public async Task AddAsync(Avis entity)
        {
            await clubMedDBContext.Avis.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Avis avis, Avis entity)
        {
            clubMedDBContext.Entry(avis).State = EntityState.Modified;
            avis.AvisId = entity.AvisId;
            avis.ResortId = entity.ResortId;
            avis.ClientId = entity.ClientId;
            avis.PhotoId = entity.PhotoId;
            avis.Note = entity.Note;
            avis.Commentaire = entity.Commentaire;
            avis.Date = entity.Date;
            avis.LeResort = avis.LeResort;
            avis.LeClient = avis.LeClient;
            avis.LaPhoto = avis.LaPhoto;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Avis entity)
        {
            clubMedDBContext.Avis.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
