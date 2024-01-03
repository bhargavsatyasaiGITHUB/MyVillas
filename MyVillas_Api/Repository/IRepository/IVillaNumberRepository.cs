using MyVillas_Api.Models;

namespace MyVillas_Api.Repository.IRepository
{
    public interface IVillaNumberRepository:IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
