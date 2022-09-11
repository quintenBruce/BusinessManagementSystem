using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Payments",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Out_Of_Town",
                table: "Orders",
                newName: "OutOfTown");

            migrationBuilder.RenameColumn(
                name: "Order_status",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Order_fulfillment_date",
                table: "Orders",
                newName: "FulfillmentDate");

            migrationBuilder.RenameColumn(
                name: "Order_date",
                table: "Orders",
                newName: "PlacementDate");

            migrationBuilder.RenameColumn(
                name: "Order_completion_date",
                table: "Orders",
                newName: "CompletionDate");

            migrationBuilder.RenameColumn(
                name: "Com_thread",
                table: "Orders",
                newName: "ComThread");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Customers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Phone_number",
                table: "Customers",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Payments",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "Order_status");

            migrationBuilder.RenameColumn(
                name: "PlacementDate",
                table: "Orders",
                newName: "Order_date");

            migrationBuilder.RenameColumn(
                name: "OutOfTown",
                table: "Orders",
                newName: "Out_Of_Town");

            migrationBuilder.RenameColumn(
                name: "FulfillmentDate",
                table: "Orders",
                newName: "Order_fulfillment_date");

            migrationBuilder.RenameColumn(
                name: "CompletionDate",
                table: "Orders",
                newName: "Order_completion_date");

            migrationBuilder.RenameColumn(
                name: "ComThread",
                table: "Orders",
                newName: "Com_thread");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customers",
                newName: "fullName");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "Phone_number");
        }
    }
}