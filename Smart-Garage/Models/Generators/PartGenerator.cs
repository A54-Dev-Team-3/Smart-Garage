namespace Smart_Garage.Models.Generators
{
    public static class PartGenerator
    {
        internal static IList<Part> CreateParts()
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
                "Timing Belt SKF VKMA 91401",
                "Water Pump Meyle 12-34 500 6646",
                "Oil Mercedes-Benz 5W-30, 229.51, 5L.",
                "Spark Plug NGK IZFR6H11",
                "Gasket Elring 538.980",
                "Brakepads FEBI Bilstein 16666",
                "Coolant HEPU P999 G12 2L.",
                "Shock Absorber RIDEX 854S0086",
                "Oxygen Sensor Bosch 0258005726"
            };

            var UnitPrices = new double[]
            {
                15.20,
                228,84,
                95,90,
                22.12,
                311.89,
                170.84,
                16,90,
                59.46,
                144.60
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

            var parts = new List<Part>();

            for (int i = 0; i < 9; i++)
            {
                var newPart = new Part();
                newPart.Id = ids[i];
                newPart.Name = names[i];
                newPart.UnitPrice = UnitPrices[i];
                newPart.IsDeleted = areDeleted[i];

                parts.Add(newPart);
            }
            return parts;
        }
    }
}
