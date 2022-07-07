using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    public partial class RMCustomerMiddleandLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Middle_name",
                table: "Customers");

            migrationBuilder.RenameColumn("First_name", "Customers", "fullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.RenameColumn("fullName", "Customers", "First_name");

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Middle_name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
