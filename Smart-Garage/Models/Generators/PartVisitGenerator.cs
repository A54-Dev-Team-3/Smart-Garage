namespace Smart_Garage.Models.Generators
{
    public static class PartVisitGenerator
    {
        internal static IList<PartVisit> CreateVisitParts()
        {

            var partIds = new int[]
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

            var partsVisit = new List<PartVisit>();

            for (int i = 0; i < 10; i++)
            {
                var newPartVisit = new PartVisit();
                newPartVisit.PartId = partIds[i];
                newPartVisit.VisitId = visitIds[i];

                partsVisit.Add(newPartVisit);
            }
            return partsVisit;
        }
    }
}
