namespace HangmanResponse.Domain.Interfaces
{
    public interface IAuditable
    {
        void AddNotification(Notifications.Notification notification);
        void ClearLogs();
        IReadOnlyCollection<Notifications.Notification> GetNotifications();
    }
}
