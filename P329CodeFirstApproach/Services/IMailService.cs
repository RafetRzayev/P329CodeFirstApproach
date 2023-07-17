using P329CodeFirstApproach.Data;

namespace P329CodeFirstApproach.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(RequestEmail requestEmail);
    }
}
