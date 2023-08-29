namespace Helper.Infrastructure.Integrations
{
    public interface IGoogleDriveClient
    {
        Task CreateFolder(string FolderName);
    }
}
