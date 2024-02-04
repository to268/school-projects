using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class TypeChambreManager : IDataRepository<TypeChambre>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public TypeChambreManager() { }
        public TypeChambreManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeChambre>>> GetAllAsync()
        {
            return await clubMedDBContext.TypeChambres
                    .Include("LesPointForts")
                    .Include("LesCommodites")
                    .ToListAsync();
        }
        public async Task<ActionResult<TypeChambre>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.TypeChambres
                    .Include("LesPointForts")
                    .Include("LesCommodites")
                    .FirstOrDefaultAsync(u => u.TypeChambreId == id);
        }

        public async Task<ActionResult<IEnumerable<TypeChambre>>> GetByIntInf(int inf)
        {
            return await clubMedDBContext.TypeChambres
                    .Include("LesPointForts")
                    .Include("LesCommodites")
                    .Where(u => u.PrixJournalier <= inf).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<TypeChambre>>> GetByIntSup(int sup)
        {
            return await clubMedDBContext.TypeChambres
                    .Include("LesPointForts")
                    .Include("LesCommodites")
                    .Where(u => u.PrixJournalier >= sup).ToListAsync();
        }

        public async Task<ActionResult<TypeChambre>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.TypeChambres
                    .Include("LesPointForts")
                    .Include("LesCommodites")
                    .FirstOrDefaultAsync(u => u.LibelleCatgorie.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(TypeChambre entity)
        {
            await clubMedDBContext.TypeChambres.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeChambre typeChambre, TypeChambre entity)
        {
            clubMedDBContext.Entry(typeChambre).State = EntityState.Modified;
            typeChambre.TypeChambreId = entity.TypeChambreId;
            typeChambre.ResortId = entity.ResortId;
            typeChambre.CategorieTypeChambreId = entity.CategorieTypeChambreId;
            typeChambre.Surface = entity.Surface;
            typeChambre.Capacite = entity.Capacite;
            typeChambre.Presentation = entity.Presentation;
            typeChambre.LibelleCatgorie = entity.LibelleCatgorie;
            typeChambre.PrixJournalier = entity.PrixJournalier;
            typeChambre.LaCategorieTypeChambre = typeChambre.LaCategorieTypeChambre;
            typeChambre.LesPointForts = typeChambre.LesPointForts;
            typeChambre.LesCommodites = typeChambre.LesCommodites;
            typeChambre.LeResort = typeChambre.LeResort;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeChambre typeChambre)
        {
            clubMedDBContext.TypeChambres.Remove(typeChambre);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
