namespace ShiftManagementSystem.Services
{
    public interface IEmailServices
    {
        bool SendEmail(string to, string subject, string html);
    }
}
