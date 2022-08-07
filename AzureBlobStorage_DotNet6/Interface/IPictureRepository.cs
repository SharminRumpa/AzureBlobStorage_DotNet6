using AzureBlobStorage_DotNet6.Models;

namespace AzureBlobStorage_DotNet6.Interface
{
    public interface IPictureRepository
    {
        //List<Picture> GetAllPicture();
        dynamic GetAllPicture();
        dynamic SavePicture(Picture model);
        
    }
}
