using AzureBlobStorage_DotNet6.Models;
namespace AzureBlobStorage_DotNet6.Interface
{
    public interface IPictureRepositoryBLR : IBLRBase
    {
        Task<Picture> GetPicture(int id);
        Task<Picture> SavePicture(Picture model);
        Task<string> DeletePicture(int id);       
    }
}
