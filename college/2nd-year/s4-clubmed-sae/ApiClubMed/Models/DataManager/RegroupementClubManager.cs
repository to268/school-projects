using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class RegroupementClubManager : IDataRepository<RegroupementClub>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public RegroupementClubManager() { }
        public RegroupementClubManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<RegroupementClub>>> GetAllAsync()
        {
            return await clubMedDBContext.RegroupementClubs
                    //.Include("LesResorts")
                    .ToListAsync();
        }
        public async Task<ActionResult<RegroupementClub>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.RegroupementClubs
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.RegroupementClubId == id);
        }
        public async Task<ActionResult<RegroupementClub>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.RegroupementClubs
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(RegroupementClub entity)
        {
            await clubMedDBContext.RegroupementClubs.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(RegroupementClub regroupementClub, RegroupementClub entity)
        {
            clubMedDBContext.Entry(regroupementClub).State = EntityState.Modified;
            regroupementClub.RegroupementClubId = entity.RegroupementClubId;
            regroupementClub.Libelle = entity.Libelle;
            regroupementClub.LesResorts = regroupementClub.LesResorts;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(RegroupementClub regroupementClub)
        {
            clubMedDBContext.RegroupementClubs.Remove(regroupementClub);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
