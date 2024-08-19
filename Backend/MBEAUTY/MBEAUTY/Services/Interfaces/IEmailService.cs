namespace MBEAUTY.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string from, string to, string body, string subject);
    }
}
