namespace WPF_Starter.Services.DialogServices.Interfaces
{
    public interface IFileDialogService
    {
        string? ChooseFile(string filter, string title);
        string? CreateFile(string filter, string title);
    }
}
