using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class ClientManager : IDataRepository<Client>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public ClientManager() { }
        public ClientManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Client>>> GetAllAsync()
        {
            return await clubMedDBContext.Clients
                    .Include("LesReservations")
                    .Include("LesCarteBanquaires")
                    .Include("LesAvis")
                    .Include("LaCivilite")
                    .Include("LePays")
                    .ToListAsync();
        }
        public async Task<ActionResult<Client>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Clients
                    .Include("LesReservations")
                    .Include("LesCarteBanquaires")
                    .Include("LesAvis")
                    .Include("LaCivilite")
                    .Include("LePays")
                    .FirstOrDefaultAsync(u => u.ClientId == id);
        }

        public async Task<ActionResult<Client>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Clients
                    .Include("LesReservations")
                    .Include("LesCarteBanquaires")
                    .Include("LesAvis")
                    .Include("LaCivilite")
                    .Include("LePays")
                    .FirstOrDefaultAsync(u => u.Email.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Client entity)
        {
            await clubMedDBContext.Clients.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Client client, Client entity)
        {
            clubMedDBContext.Entry(client).State = EntityState.Modified;
            client.ClientId = entity.ClientId;
            client.PaysId = entity.PaysId;
            client.CiviliteId = entity.CiviliteId;
            client.Prenom = entity.Prenom;
            client.Nom = entity.Nom;
            client.DateNaissance = entity.DateNaissance;
            client.Email = entity.Email;
            client.Tel = entity.Tel;
            client.NumRue = entity.NumRue;
            client.NomRue = entity.NomRue;
            client.CodePostal = entity.CodePostal;
            client.Ville = entity.Ville;
            client.Password = entity.Password;
            client.DerniereConnexion = entity.DerniereConnexion;
            client.TempsRestant = entity.TempsRestant;
            client.LesReservations = client.LesReservations;
            client.LesCarteBanquaires = client.LesCarteBanquaires;
            client.LesAvis = client.LesAvis;
            client.LaCivilite = client.LaCivilite;
            client.LePays = client.LePays;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Client entity)
        {
            clubMedDBContext.Clients.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
