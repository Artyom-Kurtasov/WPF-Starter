using System.Windows;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.MessageServices
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string message, string caption, MessageBoxImage image, MessageBoxButton button)
        {
            MessageBox.Show(message, caption, button, image);
        }
    }
}
