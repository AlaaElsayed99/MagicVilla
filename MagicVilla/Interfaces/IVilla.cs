
namespace MagicVilla.Interfaces
{
    public interface IVilla: IRepository<VillaAPI>
    {
        Task<VillaAPI> UpdateAsync(VillaAPI villa);
    }
}
