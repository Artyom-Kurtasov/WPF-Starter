using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WPF_Starter.Services.MessageServices.Interfaces
{
    public interface IMessageBoxService
    {
        Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style);
    }
}
