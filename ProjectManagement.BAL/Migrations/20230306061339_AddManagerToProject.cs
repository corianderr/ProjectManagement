using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.BAL.Migrations
{
    public partial class AddManagerToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_ProjectsId",
                table: "EmployeeProject");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "EmployeeProject",
                newName: "WorkedOnProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_WorkedOnProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ManagerId",
                table: "Project",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_WorkedOnProjectsId",
                table: "EmployeeProject",
                column: "WorkedOnProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_ManagerId",
                table: "Project",
                column: "ManagerId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_WorkedOnProjectsId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_ManagerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ManagerId",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "WorkedOnProjectsId",
                table: "EmployeeProject",
                newName: "ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_WorkedOnProjectsId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
