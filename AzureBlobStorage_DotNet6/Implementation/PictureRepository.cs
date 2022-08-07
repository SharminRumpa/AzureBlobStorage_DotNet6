using AzureBlobStorage_DotNet6.Data;
using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureBlobStorage_DotNet6.Implementation
{
    public class PictureRepository: IPictureRepository
    {
        private readonly ApplicationDbContext _context;
        public PictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public List<Picture> GetAllPicture()
        //{
        //    return _context.Picture.ToList();
        //}

        dynamic IPictureRepository.GetAllPicture()
        {
            return _context.Picture.ToList();
        }

        public dynamic SavePicture(Picture model)
        {
            ResponseVM responseVM = new ResponseVM();
            try
            {
                _context.Add<Picture>(model);

                responseVM.Messsage = "Inserted Successfully";

                _context.SaveChanges();
                responseVM.IsSuccess = true;

            }
            catch (Exception ex)
            {
                responseVM.IsSuccess = false;
                responseVM.Messsage = "Error : " + ex.Message;
            }
            return responseVM;
        }

        
    }
}
