namespace Helper.Infrastructure.Integrations
{
    public interface IGoogleDriveClient
    {
        Task<Google.Apis.Drive.v3.DriveService> GetService_v3();
        Task CreateFolder(string FolderName);
    }
}
