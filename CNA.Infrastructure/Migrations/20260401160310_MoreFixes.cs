using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoreFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId1",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ProductVariants_ProductVariantId1",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantAttributes_ProductVariants_ProductVariantId1",
                table: "VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_VariantAttributes_ProductVariantId1",
                table: "VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductVariantId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId1",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "ProductVariantId1",
                table: "VariantAttributes");

            migrationBuilder.DropColumn(
                name: "ProductVariantId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductVariants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantId1",
                table: "VariantAttributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantId1",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "ProductVariants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttributes_ProductVariantId1",
                table: "VariantAttributes",
                column: "ProductVariantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductVariantId1",
                table: "Reviews",
                column: "ProductVariantId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId1",
                table: "ProductVariants",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId1",
                table: "ProductVariants",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ProductVariants_ProductVariantId1",
                table: "Reviews",
                column: "ProductVariantId1",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantAttributes_ProductVariants_ProductVariantId1",
                table: "VariantAttributes",
                column: "ProductVariantId1",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }
    }
}
