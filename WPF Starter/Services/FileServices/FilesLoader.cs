using WPF_Starter.Services.DialogServices.Interfaces;

namespace WPF_Starter.Services.FileServices
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
