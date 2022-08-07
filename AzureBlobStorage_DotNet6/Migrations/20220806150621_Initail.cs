using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureBlobStorage_DotNet6.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SeoFilename = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AltAttribute = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TitleAttribute = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
