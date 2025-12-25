namespace WPF_Starter.Services.Import.Interfaces
{
    public interface IImportCsv
    {
        event EventHandler? InvalidPath;
        event EventHandler? ImportCsvFailed;
        event EventHandler? ImportCompleted;
        event EventHandler? InvalidConnectionString;

        Task Import();
    }
}
