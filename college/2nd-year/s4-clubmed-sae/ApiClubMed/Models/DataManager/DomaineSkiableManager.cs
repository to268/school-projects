using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiClubMed.Models.DataManager
{
    public class DomaineSkiableManager : IDataRepository<DomaineSkiable>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public DomaineSkiableManager() { }
        public DomaineSkiableManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            clubMedDBContext.DomaineSkiables.Include(c => c.LesResorts).ToList();
        }
        public async Task<ActionResult<IEnumerable<DomaineSkiable>>> GetAllAsync()
        {
            return await clubMedDBContext.DomaineSkiables.ToListAsync();
        }
        public async Task<ActionResult<DomaineSkiable>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.DomaineSkiables.FirstOrDefaultAsync(u => u.DomaineSkiableId == id);
        }
        public async Task<ActionResult<DomaineSkiable>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.DomaineSkiables.FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(DomaineSkiable entity)
        {
            await clubMedDBContext.DomaineSkiables.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DomaineSkiable domaineSkiable, DomaineSkiable entity)
        {
            clubMedDBContext.Entry(domaineSkiable).State = EntityState.Modified;
            domaineSkiable.DomaineSkiableId = entity.DomaineSkiableId;
            domaineSkiable.PhotoId = entity.PhotoId;
            domaineSkiable.Titre = entity.Titre;
            domaineSkiable.Nom = entity.Nom;
            domaineSkiable.AltitudeClub = entity.AltitudeClub;
            domaineSkiable.AltitudeStation = entity.AltitudeStation;
            domaineSkiable.NbPiste = entity.NbPiste;
            domaineSkiable.InfoSkiAuPied = entity.InfoSkiAuPied;
            domaineSkiable.Description = entity.Description;
            domaineSkiable.LongueurDesPistes = entity.LongueurDesPistes;
            domaineSkiable.LaPhoto = domaineSkiable.LaPhoto;
            domaineSkiable.LesResorts = domaineSkiable.LesResorts;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(DomaineSkiable domaineSkiable)
        {
            clubMedDBContext.DomaineSkiables.Remove(domaineSkiable);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
