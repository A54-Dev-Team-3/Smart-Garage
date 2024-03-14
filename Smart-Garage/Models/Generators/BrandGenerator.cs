namespace Smart_Garage.Models.Generators
{
    public static class BrandGenerator
    {
        internal static IList<VehicleBrand> CreateBrands()
        {
            var ids = new int[]
            {
                1,
                2,
                3
            };

            var names = new string[]
            {
                "Mercedes-Benz",
                "BMW",
                "Skoda"
            };

            var areDeleted = new bool[]
            {
                false,
                false,
                false
            };

            var brands = new List<VehicleBrand>();

            for (int i = 0; i < 3; i++)
            {
                var newBrand = new VehicleBrand();
                newBrand.Id = ids[i];
                newBrand.Name = names[i];
                newBrand.IsDeleted = areDeleted[i];

                brands.Add(newBrand);
            }
            return brands;
        }
    }
}
