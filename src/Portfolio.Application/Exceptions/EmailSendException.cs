namespace Portfolio.Application.Exceptions
{
    /// <summary>
    /// Excepción cuando falla el envío de email.
    /// </summary>
    public class EmailSendException : Exception
    {
        public EmailSendException(string message) : base(message)
        {
        }
    }
}
