using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Starter.ViewModels.Interfaces
{
    public interface IFileDialogService
    {
        string? ChooseFile(string filter, string title);
        string? CreateFile(string filter, string title);
    }
}
