using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WPF_Starter.Services.MessageServices.Interfaces
{
    public interface IMessageBoxService
    {
        void ShowMessage(string message, string caption, MessageBoxImage image, MessageBoxButton button);
    }
}
