﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockControl.Server.Data;

#nullable disable

namespace StockControl.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240414171124_AddActiveInEquipos")]
    partial class AddActiveInEquipos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EquiposSpareParts", b =>
                {
                    b.Property<int>("EquiposId_Equip")
                        .HasColumnType("int");

                    b.Property<int>("SparePartsId_Code")
                        .HasColumnType("int");

                    b.HasKey("EquiposId_Equip", "SparePartsId_Code");

                    b.HasIndex("SparePartsId_Code");

                    b.ToTable("EquiposSpareParts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("StockControl.Shared.Models.Equipos", b =>
                {
                    b.Property<int>("Id_Equip")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Equip"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NameEquip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Equip");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("StockControl.Shared.Models.EquiposRepuestos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipoId_Equip")
                        .HasColumnType("int");

                    b.Property<int>("SparePartsId_Code")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipoId_Equip");

                    b.HasIndex("SparePartsId_Code");

                    b.ToTable("EquiposRepuestos");
                });

            modelBuilder.Entity("StockControl.Shared.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("CI")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("StockControl.Shared.Models.Movements", b =>
                {
                    b.Property<int>("Id_Movement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Movement"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Hour")
                        .HasColumnType("datetime2");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("SparePartsId_Code")
                        .HasColumnType("int");

                    b.Property<int>("TipoStockId_Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("TotalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserNameId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id_Movement");

                    b.HasIndex("SparePartsId_Code");

                    b.HasIndex("TipoStockId_Stock");

                    b.HasIndex("UserNameId");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("StockControl.Shared.Models.OrdenesTotales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EquipoId_Equip")
                        .HasColumnType("int");

                    b.Property<int>("Escuela")
                        .HasColumnType("int");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("isFinished")
                        .HasColumnType("int");

                    b.Property<bool>("isFlash")
                        .HasColumnType("bit");

                    b.Property<bool>("isFlashCap")
                        .HasColumnType("bit");

                    b.Property<string>("nOrden")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EquipoId_Equip");

                    b.HasIndex("UserNameId");

                    b.ToTable("OrdenesTotales");
                });

            modelBuilder.Entity("StockControl.Shared.Models.OrderSpareParts", b =>
                {
                    b.Property<int>("SparePartId")
                        .HasColumnType("int");

                    b.Property<int>("OrderTotalId")
                        .HasColumnType("int");

                    b.Property<int>("OrdersTotalsId")
                        .HasColumnType("int");

                    b.Property<int>("SparePartsId_Code")
                        .HasColumnType("int");

                    b.Property<int>("TipoStockId_Stock")
                        .HasColumnType("int");

                    b.HasKey("SparePartId", "OrderTotalId");

                    b.HasIndex("OrdersTotalsId");

                    b.HasIndex("SparePartsId_Code");

                    b.HasIndex("TipoStockId_Stock");

                    b.ToTable("OrderSpareParts");
                });

            modelBuilder.Entity("StockControl.Shared.Models.RepuestosEstados", b =>
                {
                    b.Property<int>("SparePartId")
                        .HasColumnType("int");

                    b.Property<int>("StockTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("SparePartId", "StockTypeId");

                    b.HasIndex("StockTypeId");

                    b.ToTable("RepuestosEstados");
                });

            modelBuilder.Entity("StockControl.Shared.Models.SpareParts", b =>
                {
                    b.Property<int>("Id_Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Code"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Code");

                    b.ToTable("SpareParts");
                });

            modelBuilder.Entity("StockControl.Shared.Models.TipoStock", b =>
                {
                    b.Property<int>("Id_Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Stock"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NameStock")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Stock");

                    b.ToTable("TipoStock");
                });

            modelBuilder.Entity("EquiposSpareParts", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Equipos", null)
                        .WithMany()
                        .HasForeignKey("EquiposId_Equip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.SpareParts", null)
                        .WithMany()
                        .HasForeignKey("SparePartsId_Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StockControl.Shared.Models.EquiposRepuestos", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Equipos", "Equipo")
                        .WithMany()
                        .HasForeignKey("EquipoId_Equip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.SpareParts", "SpareParts")
                        .WithMany()
                        .HasForeignKey("SparePartsId_Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipo");

                    b.Navigation("SpareParts");
                });

            modelBuilder.Entity("StockControl.Shared.Models.Movements", b =>
                {
                    b.HasOne("StockControl.Shared.Models.SpareParts", "SpareParts")
                        .WithMany()
                        .HasForeignKey("SparePartsId_Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.TipoStock", "TipoStock")
                        .WithMany()
                        .HasForeignKey("TipoStockId_Stock")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", "UserName")
                        .WithMany()
                        .HasForeignKey("UserNameId");

                    b.Navigation("SpareParts");

                    b.Navigation("TipoStock");

                    b.Navigation("UserName");
                });

            modelBuilder.Entity("StockControl.Shared.Models.OrdenesTotales", b =>
                {
                    b.HasOne("StockControl.Shared.Models.Equipos", "Equipo")
                        .WithMany()
                        .HasForeignKey("EquipoId_Equip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.Identity.ApplicationUser", "UserName")
                        .WithMany()
                        .HasForeignKey("UserNameId");

                    b.Navigation("Equipo");

                    b.Navigation("UserName");
                });

            modelBuilder.Entity("StockControl.Shared.Models.OrderSpareParts", b =>
                {
                    b.HasOne("StockControl.Shared.Models.OrdenesTotales", "OrdersTotals")
                        .WithMany("SpareParts")
                        .HasForeignKey("OrdersTotalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.SpareParts", "SpareParts")
                        .WithMany()
                        .HasForeignKey("SparePartsId_Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.TipoStock", "TipoStock")
                        .WithMany()
                        .HasForeignKey("TipoStockId_Stock")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrdersTotals");

                    b.Navigation("SpareParts");

                    b.Navigation("TipoStock");
                });

            modelBuilder.Entity("StockControl.Shared.Models.RepuestosEstados", b =>
                {
                    b.HasOne("StockControl.Shared.Models.SpareParts", "SpareParts")
                        .WithMany()
                        .HasForeignKey("SparePartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockControl.Shared.Models.TipoStock", "TipoStock")
                        .WithMany()
                        .HasForeignKey("StockTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpareParts");

                    b.Navigation("TipoStock");
                });

            modelBuilder.Entity("StockControl.Shared.Models.OrdenesTotales", b =>
                {
                    b.Navigation("SpareParts");
                });
#pragma warning restore 612, 618
        }
    }
}
