using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlobStorage_DotNet6.Implementation
{
    public class AzureBlobStorageBLR : IAzureBlobStorageBLR
    {
        private readonly AzureBlobStorage _configuration;
        public AzureBlobStorageBLR(IOptions<AzureBlobStorage> configuration)
        {
            _configuration = configuration.Value;
        }
        public async Task<dynamic> SaveAzureBlobStorageFile(IFormFile file, string fileName, string contentType)
        {           
            try
            {
                // Retrieve storage account from connection string.
                string blobstorageconnection = _configuration.AzureConnectionString;
                string containerName = _configuration.ContainerName;
                string sourceFolder = _configuration.SourceFolder;

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                // Create the blob client. 
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container. 
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                container.CreateIfNotExistsAsync().Wait();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                blockBlob.Properties.ContentType = contentType;

                await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
              
                return blockBlob.StorageUri.PrimaryUri.ToString();
                //return "Save successfully.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<dynamic> DownloadFile(string fileName)
        {
            try
            {
                // Retrieve storage account from connection string.
                string blobstorageconnection = _configuration.AzureConnectionString;
                string containerName = _configuration.ContainerName;
                string sourceFolder = _configuration.SourceFolder;

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                CloudBlockBlob blockBlob;

                await using (MemoryStream memoryStream = new MemoryStream())
                {
                    blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    await blockBlob.DownloadToStreamAsync(memoryStream);
                }
                Stream blobStream = blockBlob.OpenReadAsync().Result;
                //return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
                //return (blobStream, blockBlob.Properties.ContentType, blockBlob.Name);

                var data = new
                {
                    blobStream = blobStream,
                    contentType = blockBlob.Properties.ContentType,
                    fileName = blockBlob.Name
                };
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<dynamic> DeleteAzureBlobStorageFile(string fileName)
        {
            try
            {
                // Retrieve storage account from connection string.
                string blobstorageconnection = _configuration.AzureConnectionString;
                string containerName = _configuration.ContainerName;
                string sourceFolder = _configuration.SourceFolder;

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                var blob = cloudBlobContainer.GetBlobReference(fileName);
                await blob.DeleteIfExistsAsync();
                return "File Deleted";
            }
            catch (Exception)
            {
                throw;
            }           
        }

        
    }
}
