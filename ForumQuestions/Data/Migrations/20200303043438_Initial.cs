using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumQuestions.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<string>(nullable: true),
                    QuestionHeader = table.Column<string>(nullable: true),
                    QuestionBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Question_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    ReplyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionPostQuestionID = table.Column<int>(nullable: true),
                    MemberId = table.Column<string>(nullable: true),
                    ReplyBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_Reply_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reply_Question_QuestionPostQuestionID",
                        column: x => x.QuestionPostQuestionID,
                        principalTable: "Question",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_MemberId",
                table: "Question",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_MemberId",
                table: "Reply",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_QuestionPostQuestionID",
                table: "Reply",
                column: "QuestionPostQuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
