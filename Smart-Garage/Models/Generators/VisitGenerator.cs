namespace Smart_Garage.Models.Generators
{
    public class VisitGenerator
    {
        internal static IList<Visit> CreateVisits()
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
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
            };

            var vehicleIds = new int[]
            {
                1,
                1,
                2,
                2,
                3,
                3,
                4,
                4,
                5,
                5,
                6,
                6,
                7,
                7,
                8,
                8,
                9,
                9
            };

            var dates = new DateTime[]
            {
                DateTime.ParseExact("14.08.2019", "dd.MM.yyyy", null),
                DateTime.ParseExact("03.02.2017", "dd.MM.yyyy", null),
                DateTime.ParseExact("22.05.2021", "dd.MM.yyyy", null),
                DateTime.ParseExact("09.11.2015", "dd.MM.yyyy", null),
                DateTime.ParseExact("28.07.2018", "dd.MM.yyyy", null),
                DateTime.ParseExact("17.03.2016", "dd.MM.yyyy", null),
                DateTime.ParseExact("05.06.2022", "dd.MM.yyyy", null),
                DateTime.ParseExact("24.12.2020", "dd.MM.yyyy", null),
                DateTime.ParseExact("13.09.2017", "dd.MM.yyyy", null),
                DateTime.ParseExact("02.01.2023", "dd.MM.yyyy", null),
                DateTime.ParseExact("25.09.2019", "dd.MM.yyyy", null),
                DateTime.ParseExact("18.04.2017", "dd.MM.yyyy", null),
                DateTime.ParseExact("07.11.2021", "dd.MM.yyyy", null),
                DateTime.ParseExact("30.06.2015", "dd.MM.yyyy", null),
                DateTime.ParseExact("13.02.2018", "dd.MM.yyyy", null),
                DateTime.ParseExact("09.10.2016", "dd.MM.yyyy", null),
                DateTime.ParseExact("01.03.2022", "dd.MM.yyyy", null),
                DateTime.ParseExact("24.08.2020", "dd.MM.yyyy", null),
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
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
            };

            var visits = new List<Visit>();

            for (int i = 0; i < ids.Length; i++)
            {
                var newVisits = new Visit();
                newVisits.Id = ids[i];
                newVisits.VehicleId = vehicleIds[i];
                newVisits.Date = dates[i];
                newVisits.IsDeleted = areDeleted[i];

                visits.Add(newVisits);
            }

            return visits;
        }
    }
}
