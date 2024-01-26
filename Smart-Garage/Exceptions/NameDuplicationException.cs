namespace Smart_Garage.Exceptions
{
    public class NameDuplicationException : ApplicationException
    {
        public NameDuplicationException(string message)
            : base(message)
        {

        }
    }
}
