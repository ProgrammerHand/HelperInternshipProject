namespace Helper.Application.Integrations
{
    public interface IGoogleDriveClient
    {
        Task<string> CreateFolder(string FolderName);
    }
}
