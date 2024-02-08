namespace Smart_Garage.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Model> Models { get; set; } = new HashSet<Model>();
    }
}
