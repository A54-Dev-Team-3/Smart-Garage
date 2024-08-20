namespace Smart_Garage.Models.Generators
{
    public static class ServiceGenerator
    {
        internal static IList<Service> CreateServices()
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
                "Change Timing Belt",
                "Change Water Pump",
                "Change Oil",
                "Change Spark Plugs",
                "Change Gasket",
                "Change Brakepads",
                "Change Coolant",
                "Change Shock Absorber",
                "Change Oxygen Sensor",
            };

            var prices = new double[]
            {
                25,
                100,
                20,
                15,
                125,
                30,
                5,
                50,
                10
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

            var serevices = new List<Service>();

            for (int i = 0; i < 9; i++)
            {
                var newPart = new Service();
                newPart.Id = ids[i];
                newPart.Name = names[i];
                newPart.Price = prices[i];
                newPart.IsDeleted = areDeleted[i];

                serevices.Add(newPart);
            }
            return serevices;
        }
    }
}