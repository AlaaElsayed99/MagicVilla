namespace MagicVilla.DTOs
{
    public class VillaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime UpdatedDate { get; set; }= DateTime.Now;
        public string Details { get; set; }
        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public double Rate { get; set; }
    }
}
