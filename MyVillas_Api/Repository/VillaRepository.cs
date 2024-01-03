using Microsoft.EntityFrameworkCore;
using MyVillas_Api.Data;
using MyVillas_Api.Models;
using MyVillas_Api.Repository.IRepository;
using System.Linq.Expressions;

namespace MyVillas_Api.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
      

     

       
       
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
