using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ApiClubMed.Models.DataManager
{
    public class ResortManager : IDataRepository<Resort>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ResortManager() { }
        public ResortManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Resort>>> GetAllAsync()
        {
            return await clubMedDBContext.Resorts
                    .Include("LesPhotos")
                    .Include("LesVideos")
                    .Include("LesTypeClubs")
                    .Include("LesRegroupementClubs")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteIncluses")
                    .Include("LesActiviteEnfantCartes")
                    .Include("LesActiviteEnfantIncluses")
                    .ToListAsync();
        }
        public async Task<ActionResult<Resort>> GetByIdAsync(int id)
        {

            return await clubMedDBContext.Resorts.Include("LesPhotos")
                    .Include("LesTypeChambres")
                    .Include("LesVideos")
                    .Include("LesAvis")
                    .Include("LesTypeClubs")
                    .Include("LesRegroupementClubs")
                    .Include("LesReservations")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteIncluses")
                    .Include("LesActiviteEnfantCartes")
                    .Include("LesActiviteEnfantIncluses")
                    .Include("LesBars")
                    .Include("LesRestaurants")
                    .Include("LeDomaineSkiable")
                    .Include("LaLocalisation")
                    .Include("LaSousLocalisation")
                    .FirstOrDefaultAsync(u => u.ResortId == id);
        }
        public async Task<ActionResult<Resort>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Resorts.Include("LesPhotos")
                    .Include("LesTypeChambres")
                    .Include("LesVideos")
                    .Include("LesAvis")
                    .Include("LesTypeClubs")
                    .Include("LesRegroupementClubs")
                    .Include("LesReservations")
                    .Include("LesActiviteCartes")
                    .Include("LesActiviteIncluses")
                    .Include("LesActiviteEnfantCartes")
                    .Include("LesActiviteEnfantIncluses")
                    .Include("LesBars")
                    .Include("LesRestaurants")
                    .Include("LeDomaineSkiable")
                    .Include("LaLocalisation")
                    .Include("LaSousLocalisation")
                    .FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Resort entity)
        {
            await clubMedDBContext.Resorts.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Resort resort, Resort entity)
        {
            clubMedDBContext.Entry(resort).State = EntityState.Modified;
            resort.ResortId = entity.ResortId;
            resort.Nom = entity.Nom;
            resort.MoyenneAvis = entity.MoyenneAvis;
            resort.DescriptionGen = entity.DescriptionGen;
            resort.PrixDepart = entity.PrixDepart;
            resort.LienPdfDocClub = entity.LienPdfDocClub;
            resort.LaSousLocalisation = resort.LaSousLocalisation;
            resort.LesActiviteEnfantCartes = resort.LesActiviteEnfantCartes;
            resort.LesActiviteEnfantIncluses = resort.LesActiviteEnfantIncluses;
            resort.LeDomaineSkiable = resort.LeDomaineSkiable;
            resort.LaLocalisation = resort.LaLocalisation;
            resort.LesBars = resort.LesBars;
            resort.LesRestaurants = resort.LesRestaurants;
            resort.LesActiviteCartes = resort.LesActiviteCartes;
            resort.LesActiviteIncluses = resort.LesActiviteIncluses;
            resort.LesReservations = resort.LesReservations;
            resort.LesRegroupementClubs = resort.LesRegroupementClubs;
            resort.LesTypeClubs = resort.LesTypeClubs;
            resort.LesAvis = resort.LesAvis;
            resort.LesTypeChambres = resort.LesTypeChambres;
            resort.LesVideos = resort.LesVideos;
            resort.LesPhotos = resort.LesPhotos;
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Resort resort)
        {
            clubMedDBContext.Resorts.Remove(resort);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task<ActionResult<IEnumerable<Resort>>> GetPage(uint page = 1, uint resultPerPage = uint.MaxValue)
        {
            if (page < 1)
                return null;

            uint begin = (page - 1) * resultPerPage;
            uint end = page * resultPerPage;

            Console.WriteLine("from id " + begin + " to id " + end);

            return await clubMedDBContext.Resorts
                .Where(e => e.ResortId <= end && e.ResortId > begin)
                //.Include("LesPhotos")
                //.Include("LesTypeChambres")
                //.Include("LesVideos")
                //.Include("LesAvis")
                //.Include("LesTypeClubs")
                //.Include("LesRegroupementClubs")
                //.Include("LesReservations")
                //.Include("LesActiviteCartes")
                //.Include("LesActiviteIncluses")
                //.Include("LesActiviteEnfantCartes")
                //.Include("LesActiviteEnfantIncluses")
                //.Include("LesBars")
                //.Include("LesRestaurants")
                //.Include("LeDomaineSkiable")
                //.Include("LaLocalisation")
                //.Include("LaSousLocalisation")
                .ToListAsync();
        }
    }
}
