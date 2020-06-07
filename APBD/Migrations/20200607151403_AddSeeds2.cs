using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APBD.Migrations
{
    public partial class AddSeeds2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_MedicamentIdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Prescription_PrescriptionIdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicament_MedicamentIdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicament_PrescriptionIdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.DropColumn(
                name: "MedicamentIdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropColumn(
                name: "PrescriptionIdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.AddColumn<int>(
                name: "IdPrescription",
                table: "PrescriptionMedicament",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMedicament",
                table: "PrescriptionMedicament",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament",
                columns: new[] { "IdPrescription", "IdMedicament" });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "lekarz.lekarzowski@gmail.com", "Lekarz", "Lekarzowski" },
                    { 2, "lekarz.lekarzowski2@gmail.com", "Lekarz2", "Lekarzowski2" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "LEKOWY", "LEK", "DOUSTNY" },
                    { 2, "YWOKEL", "KEL", "YNTSUOD" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 7, 17, 14, 2, 681, DateTimeKind.Local).AddTicks(7457), "Adam", "Adamowski" },
                    { 2, new DateTime(2020, 6, 7, 17, 14, 2, 688, DateTimeKind.Local).AddTicks(5172), "Adam2", "Adamowski2" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 7, 17, 14, 2, 689, DateTimeKind.Local).AddTicks(7379), new DateTime(2020, 6, 7, 17, 14, 2, 689, DateTimeKind.Local).AddTicks(7850), 1, 1 },
                    { 2, new DateTime(2020, 6, 7, 17, 14, 2, 689, DateTimeKind.Local).AddTicks(9970), new DateTime(2020, 6, 7, 17, 14, 2, 689, DateTimeKind.Local).AddTicks(9999), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicament",
                columns: new[] { "IdPrescription", "IdMedicament", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "Szczegóły", 1 },
                    { 2, 2, "Szczegóły", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdPrescription", "IdMedicament" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdPrescription", "IdMedicament" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DropColumn(
                name: "IdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.DropColumn(
                name: "IdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.AddColumn<int>(
                name: "MedicamentIdMedicament",
                table: "PrescriptionMedicament",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionIdPrescription",
                table: "PrescriptionMedicament",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicament_MedicamentIdMedicament",
                table: "PrescriptionMedicament",
                column: "MedicamentIdMedicament");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicament_PrescriptionIdPrescription",
                table: "PrescriptionMedicament",
                column: "PrescriptionIdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_MedicamentIdMedicament",
                table: "PrescriptionMedicament",
                column: "MedicamentIdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Prescription_PrescriptionIdPrescription",
                table: "PrescriptionMedicament",
                column: "PrescriptionIdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
