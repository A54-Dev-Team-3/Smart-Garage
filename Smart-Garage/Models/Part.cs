using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        public bool IsDeleted { get; set; }

        // TODO: Image ?
        public ICollection<PartVisit> PartVisits { get; set; } = new HashSet<PartVisit>();
    }
}
