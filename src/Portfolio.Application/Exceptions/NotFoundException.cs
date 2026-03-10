namespace Portfolio.Application.Exceptions
{
    /// <summary>
    /// Excepción cuando no se encuentra un recurso.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
