namespace Smart_Garage.Models.Generators
{
    public static class VehicleGenerator
    {
        internal static IList<Vehicle> CreateVehicles()
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

            var userIds = new int[]
            {
                2,
                2,
                3,
                4,
                5,
                5,
                5,
                6,
                6,
            };

            var modelIds = new int[]
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

            var licensePlates = new string[]
            {
                "CA1234HH",
                "A1500AA",
                "BH7895AA",
                "C7894AA",
                "CA9635HH",
                "OB4579AA",
                "X1597AA",
                "PA4379HH",
                "TX0598AA"
            };

            var vins = new string[]
            {
                "12345678901234567",
                "52345678901234567",
                "42344678961234567",
                "92345678991234557",
                "76543210987654321",
                "45646512637564321",
                "96385274136925845",
                "14725836925836914",
                "15948726326159487" 
            };

            var creationYears = new int[]
            {
                2005,
                2008,
                2010,
                2015,
                2013,
                2017,
                2009,
                2011,
                2020
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

            var vehicles = new List<Vehicle>();

            for (int i = 0; i < ids.Length; i++)
            {
                var newVehicles = new Vehicle();
                newVehicles.Id = ids[i];
                newVehicles.UserId = userIds[i];
                newVehicles.ModelId = modelIds[i];
                newVehicles.LicensePlate = licensePlates[i];
                newVehicles.VIN = vins[i];
                newVehicles.CreationYear = creationYears[i];
                newVehicles.IsDeleted = areDeleted[i];

                vehicles.Add(newVehicles);
            }

            return vehicles;
        }
    }
}
