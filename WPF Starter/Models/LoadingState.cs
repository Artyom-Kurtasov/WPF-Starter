using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Windows;

namespace WPF_Starter.Models
{
    public class LoadingState :INotifyPropertyChanged
    {
        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public MessageDialogResult MessageResult;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
