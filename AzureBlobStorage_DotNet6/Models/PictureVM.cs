#nullable disable
using System.ComponentModel.DataAnnotations;

namespace AzureBlobStorage_DotNet6.Models
{
    public class PictureVM
    {
        [Required]
        [StringLength(500)]
        public string TitleAttribute { get; set; }

        [Required]
        [StringLength(500)]
        public string AltAttribute { get; set; }
        
    }
}
