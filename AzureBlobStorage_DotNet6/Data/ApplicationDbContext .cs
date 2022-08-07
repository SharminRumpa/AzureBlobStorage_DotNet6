using AzureBlobStorage_DotNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureBlobStorage_DotNet6.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }
        public DbSet<Picture> Picture { get; set; }
    }
}
