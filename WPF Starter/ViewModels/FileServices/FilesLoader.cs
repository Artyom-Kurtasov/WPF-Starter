using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using WPF_Starter.Models;
using WPF_Starter.ViewModels.Interfaces;

namespace WPF_Starter.ViewModels
{
public class FilesLoader
{
    private readonly IFileDialogService _dialogService;

    public FilesLoader(IFileDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public void LoadFile(string filter, string title, Action<string?> assignToState)
    {
        var fileName = _dialogService.ChooseFile(filter, title);
        assignToState(fileName);
    }
}

}
