using MyVillas_Api.Data;
using MyVillas_Api.Models;
using MyVillas_Api.Repository.IRepository;

namespace MyVillas_Api.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }






        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.villaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
