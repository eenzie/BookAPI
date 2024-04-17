using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookAPI.Migrations
{
    /// <inheritdoc />
    public partial class createSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "Ralls, Kim" },
                    { 2, "Corets, Eva" },
                    { 3, "Randall, Cynthia" },
                    { 4, "Thurman, Paula" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "Description", "Genre", "Price", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "A former architect battles an evil sorceress.", "Fantasy", 14.95m, new DateTime(2000, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Midnight Rain" },
                    { 2, 2, "After the collapse of a nanotechnology society, the young survivors lay the foundation for a new society.", "Fantasy", 12.95m, new DateTime(2000, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maeve Ascendant" },
                    { 3, 2, "The two daughters of Maeve battle for control of England.", "Fantasy", 12.95m, new DateTime(2001, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sundered Grail" },
                    { 4, 3, "When Carla meets Paul at an ornithology conference, tempers fly.", "Romance", 7.99m, new DateTime(2000, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lover Birds" },
                    { 5, 4, "A deep sea diver finds true love 20,000 leagues beneath the sea.", "Romance", 6.99m, new DateTime(2000, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Splish Splash" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 4);
        }
    }
}
