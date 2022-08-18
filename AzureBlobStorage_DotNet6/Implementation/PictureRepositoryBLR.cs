#nullable disable
using AzureBlobStorage_DotNet6.Data;
using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureBlobStorage_DotNet6.Implementation
{
    public class PictureRepositoryBLR: IPictureRepositoryBLR
    {
        private readonly ApplicationDbContext _context;
        public PictureRepositoryBLR(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get picture by id
        public async Task<Picture> GetPicture(int id)
        {
            try
            {
                if (PictureExists(id))
                {
                    return await _context.Picture.FirstOrDefaultAsync(s => s.Id == id);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }          
        }


        // Save picture 
        public async Task<Picture> SavePicture(Picture model)
        {
            try
            {
                _context.Picture.Add(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Delete picture
        public async Task<string> DeletePicture(int id)
        {
            try
            {
                if (PictureExists(id))
                {
                    var picture = await _context.Picture.FindAsync(id);

                    if (picture != null)
                    {
                        _context.Picture.Remove(picture);
                        await _context.SaveChangesAsync();
                        return "File Deleted";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Find Picture is already exit in database?
        private bool PictureExists(int id)
        {
            try
            {
                return _context.Picture.Any(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        

        //public dynamic SavePicture(Picture model)
        //{
        //    ResponseVM responseVM = new ResponseVM();
        //    try
        //    {
        //        _context.Add<Picture>(model);

        //        responseVM.Messsage = "Inserted Successfully";

        //        _context.SaveChanges();
        //        responseVM.IsSuccess = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        responseVM.IsSuccess = false;
        //        responseVM.Messsage = "Error : " + ex.Message;
        //    }
        //    return responseVM;
        //}


    }
}
