using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class TrancheAgeManager : IDataRepository<TrancheAge>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public TrancheAgeManager() { }
        public TrancheAgeManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<TrancheAge>>> GetAllAsync()
        {
            return await clubMedDBContext.TrancheAges.ToListAsync();
        }
        public async Task<ActionResult<TrancheAge>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<TrancheAge>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.TrancheAges.FirstOrDefaultAsync(u => u.TrancheAgeId == id);
        }
        public async Task<ActionResult<IEnumerable<TrancheAge>>> GetByAgeMinAsync(int ageMin)
        {
            return await clubMedDBContext.TrancheAges.Where(u => u.AgeMin == ageMin).ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<TrancheAge>>> GetByAgeMaxAsync(int ageMax)
        {
            return await clubMedDBContext.TrancheAges.Where(u => u.AgeMin == ageMax).ToListAsync();
        }
        public async Task AddAsync(TrancheAge entity)
        {
            await clubMedDBContext.TrancheAges.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TrancheAge trancheAge, TrancheAge entity)
        {
            clubMedDBContext.Entry(trancheAge).State = EntityState.Modified;
            trancheAge.TrancheAgeId = entity.TrancheAgeId;
            trancheAge.AgeMin = entity.AgeMin;
            trancheAge.AgeMax = entity.AgeMax;
            trancheAge.LesTypeActivites = trancheAge.LesTypeActivites;
            trancheAge.LesActiviteEnfantCartes = trancheAge.LesActiviteEnfantCartes;
            trancheAge.LesActiviteEnfantIncluses = trancheAge.LesActiviteEnfantIncluses;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TrancheAge trancheAge)
        {
            clubMedDBContext.TrancheAges.Remove(trancheAge);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
