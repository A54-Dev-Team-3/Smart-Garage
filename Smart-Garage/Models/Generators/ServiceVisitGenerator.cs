namespace Smart_Garage.Models.Generators
{
    public static class ServiceVisitGenerator
    {
        internal static IList<ServiceVisit> CreateServiceVisit()
        {

            var ServiceIds = new int[]
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
                1
            };

            var visitIds = new int[]
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
                10
            };

            var serviceVisits = new List<ServiceVisit>();

            for (int i = 0; i < 10; i++)
            {
                var newServiceVisit = new ServiceVisit();
                newServiceVisit.ServiceId = ServiceIds[i];
                newServiceVisit.VisitId = visitIds[i];

                serviceVisits.Add(newServiceVisit);
            }
            return serviceVisits;
        }
    }
}
