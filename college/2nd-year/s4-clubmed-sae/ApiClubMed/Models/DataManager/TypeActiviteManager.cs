using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class TypeActiviteManager : IDataRepository<TypeActivite>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public TypeActiviteManager() { }
        public TypeActiviteManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeActivite>>> GetAllAsync()
        {
            return await clubMedDBContext.TypeActivites
                    .Include("LesTrancheAges")
                    .ToListAsync();
        }
        public async Task<ActionResult<TypeActivite>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.TypeActivites
                    .Include("LesTrancheAges")
                    .FirstOrDefaultAsync(u => u.TypeActiviteId == id);
        }

        public async Task<ActionResult<TypeActivite>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.TypeActivites
                    .Include("LesTrancheAges")
                    .FirstOrDefaultAsync(u => u.Titre == nom);
        }
        public async Task AddAsync(TypeActivite entity)
        {
            await clubMedDBContext.TypeActivites.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeActivite typeActivite, TypeActivite entity)
        {
            clubMedDBContext.Entry(typeActivite).State = EntityState.Modified;
            typeActivite.TypeActiviteId = entity.TypeActiviteId;
            typeActivite.PhotoId = entity.PhotoId;
            typeActivite.Description = entity.Description;
            typeActivite.NbActiviteIncluse = entity.NbActiviteIncluse;
            typeActivite.NbActiviteCarte = entity.NbActiviteCarte;
            typeActivite.LaPhoto = typeActivite.LaPhoto;
            typeActivite.LesTrancheAges = typeActivite.LesTrancheAges;
            typeActivite.LesActiviteIncluses = typeActivite.LesActiviteIncluses;
            typeActivite.LesActiviteCartes = typeActivite.LesActiviteCartes;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeActivite entity)
        {
            clubMedDBContext.TypeActivites.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
