namespace Smart_Garage.Models.Generators
{
    public static class ModelGenerator
    {
        internal static IList<VehicleModel> CreateModels()
        {
            var ids = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };

            var names = new string[]
            {
                "E 270",
                "W223",
                "GLA 200",

                "x7 m60i",
                "i5 m60",
                "330e",

                "Octavia 2.0",
                "Superb 2.0",
                "Kodiaq 2.0"
            };

            var BrandIds = new int[]
            {
                1,
                1,
                1,

                2,
                2,
                2,

                3,
                3,
                3
            };

            var areDeleted = new bool[]
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false
            };

            var models = new List<VehicleModel>();

            for (int i = 0; i < 9; i++)
            {
                var newModel = new VehicleModel();
                newModel.Id = ids[i];
                newModel.Name = names[i];
                newModel.BrandId = BrandIds[i];
                newModel.IsDeleted = areDeleted[i];

                models.Add(newModel);
            }
            return models;
        }
    }
}
