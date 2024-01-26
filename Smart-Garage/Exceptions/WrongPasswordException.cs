namespace Smart_Garage.Exceptions
{
    public class WrongPasswordException : ApplicationException
    {
        public WrongPasswordException(string message)
            : base(message)
        {

        }
    }
}
