namespace Smart_Garage.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
    {
        public UnauthorizedOperationException(string message)
            : base(message)
        {

        }
    }
}
