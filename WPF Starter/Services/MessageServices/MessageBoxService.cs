using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.MessageServices
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        public MessageBoxService(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        public async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var activeWindow = Application.Current.Windows
                            .OfType<MetroWindow>()
                            .FirstOrDefault(w => w.IsActive);

            activeWindow ??= Application.Current.MainWindow as MetroWindow;

            return await activeWindow.ShowMessageAsync(title, message, style);
        }

        public async Task ShowProgressAsync(string title, string message, Func<ProgressDialogController, Task> action, bool isCancelable = true)
        {
            var activeWindow = Application.Current.Windows
                .OfType<MetroWindow>()
                .FirstOrDefault(w => w.IsActive);

            activeWindow ??= Application.Current.MainWindow as MetroWindow;

            if (activeWindow == null)
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
                return;
            }

            ProgressDialogController controller = await activeWindow.ShowProgressAsync(title, message, isCancelable: isCancelable);

            try
            { 
                await action(controller);

                if (controller.IsCanceled)
                {
                    await controller.CloseAsync();
                    return;
                }

                await controller.CloseAsync();
            }
            catch (Exception ex)
            {
                await controller.CloseAsync();
                await activeWindow.ShowMessageAsync("Error", ex.Message, MessageDialogStyle.Affirmative);
            }
        }
    }
}
