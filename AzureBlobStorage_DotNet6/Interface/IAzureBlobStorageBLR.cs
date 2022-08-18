namespace AzureBlobStorage_DotNet6.Interface
{
    public interface IAzureBlobStorageBLR : IBLRBase
    {
        Task<dynamic> SaveAzureBlobStorageFile(IFormFile file, string fileName, string contentType);
        Task<dynamic> DownloadFile(string fileName);
        Task<dynamic> DeleteAzureBlobStorageFile(string fileName);
        
    }
}
