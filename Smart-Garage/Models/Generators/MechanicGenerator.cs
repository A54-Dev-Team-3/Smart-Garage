namespace Smart_Garage.Models.Generators
{
    public static class MechanicGenerator
    {
        internal static IList<Mechanic> CreateMechanics()
        {
            var ids = new int[]
            {
                1,
                2,
                3
            };

            var names = new string[]
            {
                "Jackson Brooks",
                "Liam Thompson",
                "Mason Harper"
            };

            var areDeleted = new bool[]
            {
                false,
                false,
                false
            };

            var mechanics = new List<Mechanic>();

            for (int i = 0; i < 3; i++)
            {
                var newMechanics = new Mechanic();
                newMechanics.Id = ids[i];
                newMechanics.Name = names[i];
                newMechanics.IsDeleted = areDeleted[i];

                mechanics.Add(newMechanics);
            }
            return mechanics;
        }
    }
}
