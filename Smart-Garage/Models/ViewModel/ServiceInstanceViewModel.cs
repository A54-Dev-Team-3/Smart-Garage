namespace Smart_Garage.Models.ViewModel
{
    public class ServiceInstanceViewModel
    {
        public int PartQuantity { get; set; }
        public double PartUnitPrice { get; set; }
        public PartViewModel Part { get; set; }

        public double ServicePrice { get; set; }
        public ServiceViewModel Service { get; set; }

        public MechanicViewModel Mechanic { get; set; }
    }
}
