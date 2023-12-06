

using MagicVilla.Data;

namespace MagicVilla.Repository
{
    public class VillaRepository : Repository<VillaAPI>, IVilla
    {
        private readonly AppDbContext _context;

        public VillaRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        
        public async Task<VillaAPI> UpdateAsync(VillaAPI villa)
        {
            villa.UpdatedDate = DateTime.Now;
            _context.villaAPIs.Update(villa);
            await _context.SaveChangesAsync();
            return villa;

        }

        
    }
}
