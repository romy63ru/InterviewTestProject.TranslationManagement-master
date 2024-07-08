

using System;
using System.Threading.Tasks;

using External.ThirdParty.Services;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Interfaces; // Replace 'YourNamespace' with the actual namespace where the 'INotificationService' interface is defined.

public class NotificationServiceProvider : INotificationServiceProvider
{

    private readonly ILogger<NotificationServiceProvider> _logger;

    public NotificationServiceProvider(ILogger<NotificationServiceProvider> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendNotificationWithRetryAsync(string message, int maxAttempts = 10)
    {
        var attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                if (await new UnreliableNotificationService().SendNotification(message))
                {
                    _logger.LogInformation("Notification sent.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification.");
            }
            await Task.Delay(TimeSpan.FromSeconds(2)); // Wait before retrying
            attempt++;
        }
        _logger.LogWarning($"Failed to send notification after {maxAttempts} attempts.");
        return false;
    }
}