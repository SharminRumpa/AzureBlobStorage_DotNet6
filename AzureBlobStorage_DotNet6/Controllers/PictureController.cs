#nullable disable
using AzureBlobStorage_DotNet6.Data;
using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;

namespace AzureBlobStorage_DotNet6.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IPictureRepositoryBLR _pictureBLR;
        private readonly IAzureBlobStorageBLR _blobStorageBLR;
        private readonly IConfiguration _configuration;
        public PictureController(IPictureRepositoryBLR pictureBLR, IAzureBlobStorageBLR blobStorageBLR, IConfiguration configuration)
        {
            _pictureBLR = pictureBLR;
            _blobStorageBLR = blobStorageBLR;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }       

        [HttpPost]
        public async Task<IActionResult> SavePicture([FromForm] PictureVM pictureVM)
        {
            try
            {
                IFormFile file;
                string contentType;

                if (Request.Form.Files.Count != 0)
                {                    
                    Picture model = new Picture();

                    file = Request.Form.Files[0];
                    var provider = new FileExtensionContentTypeProvider();

                    if (!provider.TryGetContentType(file.FileName, out contentType))
                    {
                        contentType = "application/octet-stream";
                    }
                    model.MimeType = contentType;
                    model.TitleAttribute = pictureVM.TitleAttribute;
                    model.AltAttribute = pictureVM.AltAttribute;
                    model.SeoFilename = pictureVM.TitleAttribute.Trim('"').Replace(" ", "-");
                    string ext = Path.GetExtension(file.FileName);
                    model.PictureUrl = _configuration.GetValue<string>("AzureBlobStorage:SourceFolder") + model.SeoFilename + ext;

                    var content = await _pictureBLR.SavePicture(model);

                    if(content != null)
                    {
                        string url = _blobStorageBLR.SaveAzureBlobStorageFile(file, content.PictureUrl, contentType).ToString();

                        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });

                        return url.GetOkResult();
                       
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }               
            }
            catch(Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(int id)
        {
            try
            {
                var picture = await _pictureBLR.GetPicture(id);

                if (picture != null)
                {
                    var result = await _blobStorageBLR.DownloadFile(picture.PictureUrl);
                    return File(result.blobStream, result.contentType, result.fileName);
                }
                return picture.GetOkResult();
            }
            catch (Exception)
            {
                throw;
            }
        }
     

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            try
            {
                var picture = await _pictureBLR.GetPicture(id);

                if(picture != null)
                {
                    var storage = await _blobStorageBLR.DeleteAzureBlobStorageFile(picture.PictureUrl);

                    if (storage == "File Deleted")
                    {
                       var result = await _pictureBLR.DeletePicture(id);

                        return result.GetOkResult();
                    }
                }
                return picture.GetOkResult();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
