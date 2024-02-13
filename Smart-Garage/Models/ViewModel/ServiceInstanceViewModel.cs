namespace Smart_Garage.Models.ViewModel
{
    public class ServiceInstanceViewModel
    {
        public MechanicViewModel Mechanic { get; set; }
        public IList<ServiceViewModel> Service { get; set; }
        public IList<PartViewModel> Parts { get; set; }
    }
}
