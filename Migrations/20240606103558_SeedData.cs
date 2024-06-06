using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_store_1_.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
          /// <inheritdoc />
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.InsertData(
        table: "AspNetRoles",
        columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp", "Discriminator" },
        values: new object[] { Guid.NewGuid().ToString(), "User", "USER", Guid.NewGuid().ToString(), "IdentityRole" }
    );

    migrationBuilder.InsertData(
        table: "AspNetRoles",
        columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp", "Discriminator" },
        values: new object[] { Guid.NewGuid().ToString(), "Admin", "ADMIN", Guid.NewGuid().ToString(), "IdentityRole" }
    );
}

/// <inheritdoc />
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.Sql("DELETE FROM [AspNetRoles] WHERE [Name] IN ('User', 'Admin')");
}
    }
}
