namespace WPF_Starter.Services.Export.Interfaces
{
    public interface IExportToXml
    {
        event EventHandler? ExportCompleted;
        event EventHandler? ExportFailed;
        event EventHandler? InvalidPath;
        event EventHandler? InvalidConnectionString;

        Task Export();
    }
}
