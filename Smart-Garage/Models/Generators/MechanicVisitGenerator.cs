namespace Smart_Garage.Models.Generators
{
    public static class MechanicVisitGenerator
    {
        internal static IList<MechanicVisit> CreateMechanicVisit()
        {
            var mechanicIds = new int[]
            {
                1,
                2,
                3,
                2,
                3,
                1,
                2,
                1,
                3,
                2
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

            var mechanicVisits = new List<MechanicVisit>();

            for (int i = 0; i < 10; i++)
            {
                var newMechanicVisit = new MechanicVisit();
                newMechanicVisit.MechanicId = mechanicIds[i];
                newMechanicVisit.VisitId = visitIds[i];

                mechanicVisits.Add(newMechanicVisit);
            }
            return mechanicVisits;
        }
    }
}
