#nullable disable
namespace AzureBlobStorage_DotNet6.Models
{
    public class AzureBlobStorage
    {
        public string AzureConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string SourceFolder { get; set; }
    }
}
