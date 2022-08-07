#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureBlobStorage_DotNet6.Models
{
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        [StringLength(250)]
        public string MimeType { get; set; }
        [StringLength(500)]
        public string SeoFilename { get; set; }
        [StringLength(500)]
        public string AltAttribute { get; set; }
        [StringLength(500)]
        public string TitleAttribute { get; set; }
    }
}
