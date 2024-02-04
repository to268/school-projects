using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class CategorieTypeChambreManager : IDataRepository<CategorieTypeChambre>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public CategorieTypeChambreManager() { }
        public CategorieTypeChambreManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<CategorieTypeChambre>>> GetAllAsync()
        {
            return await clubMedDBContext.CategorieTypeChambres.ToListAsync();
        }
        public async Task<ActionResult<CategorieTypeChambre>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.CategorieTypeChambres.FirstOrDefaultAsync(u => u.CategorieTypeChambreId == id);
        }

        public async Task<ActionResult<CategorieTypeChambre>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.CategorieTypeChambres.FirstOrDefaultAsync(u => u.Libelle.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(CategorieTypeChambre entity)
        {
            await clubMedDBContext.CategorieTypeChambres.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CategorieTypeChambre categorieTypeChambre, CategorieTypeChambre entity)
        {
            clubMedDBContext.Entry(categorieTypeChambre).State = EntityState.Modified;
            categorieTypeChambre.CategorieTypeChambreId = entity.CategorieTypeChambreId;
            categorieTypeChambre.Libelle = entity.Libelle;
            categorieTypeChambre.LesTypeChambres = categorieTypeChambre.LesTypeChambres;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CategorieTypeChambre categorieTypeChambre)
        {
            clubMedDBContext.CategorieTypeChambres.Remove(categorieTypeChambre);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
