namespace Cliente.Domain.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException(string message) : base(message) {}
    }
}
