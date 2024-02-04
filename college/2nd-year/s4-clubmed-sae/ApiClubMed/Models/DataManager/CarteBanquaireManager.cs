using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class CarteBanquaireManager : IDataRepository<CarteBanquaire>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public CarteBanquaireManager() { }
        public CarteBanquaireManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<CarteBanquaire>>> GetAllAsync()
        {
            return await clubMedDBContext.CarteBanquaires.ToListAsync();
        }
        public async Task<ActionResult<CarteBanquaire>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.CarteBanquaires.FirstOrDefaultAsync(u => u.CarteBanquaireId == id);
        }

        public async Task<ActionResult<IEnumerable<CarteBanquaire>>> GetByClientIdAsync(int id)
        {
            return await clubMedDBContext.CarteBanquaires.Where(u => u.ClientId == id).ToListAsync();
        }

        public async Task<ActionResult<CarteBanquaire>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.CarteBanquaires.FirstOrDefaultAsync(u => u.NumeroCarte.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(CarteBanquaire entity)
        {
            await clubMedDBContext.CarteBanquaires.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CarteBanquaire carteBanquaire, CarteBanquaire entity)
        {
            clubMedDBContext.Entry(carteBanquaire).State = EntityState.Modified;
            carteBanquaire.CarteBanquaireId = entity.CarteBanquaireId;
            carteBanquaire.ClientId = entity.ClientId;
            carteBanquaire.NumeroCarte = entity.NumeroCarte;
            carteBanquaire.DateExpiration = entity.DateExpiration;
            carteBanquaire.Cryptogramme = entity.Cryptogramme;
            carteBanquaire.LeClient = carteBanquaire.LeClient;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CarteBanquaire entity)
        {
            clubMedDBContext.CarteBanquaires.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}
