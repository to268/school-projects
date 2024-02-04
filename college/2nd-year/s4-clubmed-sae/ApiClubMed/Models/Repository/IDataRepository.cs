using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiClubMed.Models.Repository
{
    public interface IDataRepository<TEntity>
        where TEntity : IEntity
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);

        // This is only required to implement if we want to use the paging feature
        Task<ActionResult<IEnumerable<TEntity?>>> GetPage(uint page = 1, uint resultPerPage = uint.MaxValue)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<IEnumerable<TEntity?>>> GetByResortId(uint id)
        {
            throw new NotImplementedException();
        }
    }
}
