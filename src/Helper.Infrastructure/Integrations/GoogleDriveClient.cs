﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Helper.Application.Integrations;
using Microsoft.Extensions.Configuration;
namespace Helper.Infrastructure.Integrations
{
    public class GoogleDriveClient : IGoogleDriveClient
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        public GoogleDriveClient(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<DriveService> GetService_v3()
        {
            string[] scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };
            GoogleCredential credentialScoped;
            using (var stream = await client.GetStreamAsync(_configuration.GetValue<string>("GoogleSecretAzureStorage")))
            {
                var credential = ServiceAccountCredential.FromServiceAccountData(stream);
                credentialScoped = GoogleCredential.FromServiceAccountCredential(credential).CreateScoped(scopes);
            }
            //UserCredential credential = null;
            //using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //    credPath = Path.Combine(credPath, ".credentials/", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            //    string[] scopes = { DriveService.ScopeConstants.Drive };
            //    // Requesting Authentication or loading previously stored authentication for userName
            //     credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
            //                                                             scopes,
            //                                                             "user",
            //                                                             CancellationToken.None,
            //                                                             new FileDataStore(credPath, true)).Result;

            //    await credential.GetAccessTokenForRequestAsync();
            //}
            //var credential = await GoogleCredential.FromFileAsync("client_secret.json", CancellationToken.None);
            //credential.CreateScoped(DriveService.ScopeConstants.Drive);
            //string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
            Google.Apis.Drive.v3. DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentialScoped,
                ApplicationName = _configuration.GetValue<string>("app:name"),
            });
            return service;
        }

        public async Task<string> CreateFolder(string FolderName) 
        {
            Google.Apis.Drive.v3.DriveService service = await GetService_v3();
            var folderParent = _configuration.GetValue<string>("gdriveFolderId");

            Google.Apis.Drive.v3.Data.File FileMetaData = new Google.Apis.Drive.v3.Data.File
            {
                Name = FolderName,
                Parents = new[] { folderParent },
                MimeType = "application/vnd.google-apps.folder"
            };
            Google.Apis.Drive.v3.FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            //request.Fields = "id";
            request.Fields = "webViewLink";
            var file = request.Execute();
            return file.WebViewLink.ToString();
        }
    }
}
