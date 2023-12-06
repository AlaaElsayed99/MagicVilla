namespace MagicVilla.Interfaces
{
    public interface IVillaNumber:IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);

    }
}
