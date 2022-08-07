using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AzureBlobStorage_DotNet6.Data
{
    public static class ObjectExtensions
    {
        public static JsonResult GetOkResult(this object content, [CallerMemberName] string action = "")
        {
            var status = false;

            if (content == null)
                return new JsonResult(new { status, code = StatusCodes.Status204NoContent, action });

            int code = StatusCodes.Status200OK;
            status = true;
            return new JsonResult(new
            {
                status,
                code,
                action,
                content
            });
        }
    }
}
