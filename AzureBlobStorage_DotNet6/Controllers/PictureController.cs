using AzureBlobStorage_DotNet6.Data;
using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorage_DotNet6.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IPictureRepository _pictureRep;
        public PictureController(IPictureRepository pictureRep)
        {
            _pictureRep = pictureRep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPictureList()
        {
            try
            {
                object data = _pictureRep.GetAllPicture();
                return data.GetOkResult();
            }
            catch (Exception)
            {
                throw;
            };
        }

        [HttpPost]
        public IActionResult SavePicture(Picture model)
        {
            try
            {
                var data = _pictureRep.SavePicture(model);
                return Ok(data);
            }
            catch(Exception)
            {
                return BadRequest();
            }

        }
    }
}
