namespace Portfolio.Application.Interfaces.Services
{
    public interface IContactAuditService
    {
        Task<string> GenerateRadicateAsync(Guid messageId);
    }
}
