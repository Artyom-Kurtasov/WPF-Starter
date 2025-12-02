using Microsoft.Win32;
using WPF_Starter.Services.DialogServices.Interfaces;

namespace WPF_Starter.Services.DialogServices
{
    public class FileDialogService : IFileDialogService
    {
        public string? ChooseFile(string filter, string title)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                Title = title
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string? CreateFile(string filter, string title)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter,
                Title = title
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}
