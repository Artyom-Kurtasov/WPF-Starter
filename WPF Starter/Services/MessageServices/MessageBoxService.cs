using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.MessageServices
{
    public class MessageBoxService : IMessageBoxService
    {
        public async Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style)
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            return await metroWindow.ShowMessageAsync(title, message, style);
        }
    }
}
