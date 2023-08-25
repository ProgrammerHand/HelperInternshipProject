using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Helper.Application.Solution;
using Microsoft.Extensions.Configuration;

namespace Helper.Infrastructure.Integrations
{
    internal class GogleDriveClient : IGoogleDriveClient
    {
        private readonly IConfiguration _configuration;
        public GogleDriveClient(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<Google.Apis.Drive.v3.DriveService> GetService_v3()
        {
            //get Credentials from client_secret.json file     
            var credential = await GoogleCredential.FromFileAsync("client_secret.json", CancellationToken.None); ;
            //var CSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/");

            //using (var stream = new FileStream(Path.Combine(CSPath, "client_secret.json"), FileMode.Open, FileAccess.Read))
            //{
            //    String FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/"); ;
            //    String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(FilePath, true)).Result;
            //}

            //create Drive API service.    
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _configuration.GetValue<string>("app:name"),
            });
            return service;
        }

        public async Task CreateFolder(string FolderName) 
        {
            Google.Apis.Drive.v3.DriveService service = await GetService_v3();

            Google.Apis.Drive.v3.Data.File FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            Google.Apis.Drive.v3.FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

        }
    }
}
