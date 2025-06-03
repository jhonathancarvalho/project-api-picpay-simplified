namespace PicPaySimplified.Services.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotificationAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Notificação enviada ao cliente com sucesso.");
        }
    }
}
