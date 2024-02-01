namespace Smart_Garage.Models.QueryParameters
{
    public class ServicesQueryParameters
    {
        public string? Name { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
