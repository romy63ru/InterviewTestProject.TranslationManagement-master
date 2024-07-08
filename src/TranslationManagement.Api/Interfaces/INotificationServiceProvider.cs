using System.Threading.Tasks;

namespace TranslationManagement.Api.Interfaces;

public interface INotificationServiceProvider
{
    Task<bool> SendNotificationWithRetryAsync(string message, int maxAttempts = 10);
}