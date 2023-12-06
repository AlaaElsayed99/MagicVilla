

using MagicVilla.Data;

namespace MagicVilla.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumber
    {
        private readonly AppDbContext _context;

        public VillaNumberRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.VillaNumbers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        
    }
}
