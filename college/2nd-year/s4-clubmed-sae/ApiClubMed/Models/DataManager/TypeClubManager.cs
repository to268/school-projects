using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class TypeClubManager : IDataRepository<TypeClub>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public TypeClubManager() { }
        public TypeClubManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeClub>>> GetAllAsync()
        {
            return await clubMedDBContext.TypeClubs
                    //.Include("LesResorts")
                    .ToListAsync();
        }
        public async Task<ActionResult<TypeClub>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.TypeClubs
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.TypeClubId == id);
        }
        public async Task<ActionResult<TypeClub>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.TypeClubs
                    .Include("LesResorts")
                    .FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(TypeClub entity)
        {
            await clubMedDBContext.TypeClubs.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeClub typeClub, TypeClub entity)
        {
            clubMedDBContext.Entry(typeClub).State = EntityState.Modified;
            typeClub.TypeClubId = entity.TypeClubId;
            typeClub.Libelle = entity.Libelle;
            typeClub.LesResorts = typeClub.LesResorts;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeClub typeClub)
        {
            clubMedDBContext.TypeClubs.Remove(typeClub);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
