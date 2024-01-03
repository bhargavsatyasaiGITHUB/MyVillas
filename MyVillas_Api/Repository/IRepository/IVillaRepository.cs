using MyVillas_Api.Models;
using System.Linq.Expressions;

namespace MyVillas_Api.Repository.IRepository
{
    public interface IVillaRepository:IRepository<Villa>
    {
       
        Task<Villa> UpdateAsync(Villa entity);
      
    }
}
