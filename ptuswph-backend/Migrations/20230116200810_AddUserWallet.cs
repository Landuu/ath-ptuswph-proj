﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ptuswphbackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Wallet",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Users");
        }
    }
}
