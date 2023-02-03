﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PiecykPolHurt.DataLayer;

#nullable disable

namespace PiecykPolHurt.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230202205022_Remove-username")]
    partial class Removeusername
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.DictionaryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DictionaryTypes");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.DictionaryValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DictionaryTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DictionaryTypeId");

                    b.ToTable("DictionaryValues");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReceptionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestedReceptionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SendPointId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SendPointId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.OrderLines", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemsQuantity")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceForOneItem")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrdersLines");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.ProductSendPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ForDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SendPointId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SendPointId");

                    b.ToTable("ProductSendPoints");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.ReportDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxRow")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XmlDefinition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReportDefinitions");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.SendPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuildingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Logitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SendPoints");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.DictionaryValue", b =>
                {
                    b.HasOne("PiecykPolHurt.Model.Entities.DictionaryType", "Type")
                        .WithMany("Values")
                        .HasForeignKey("DictionaryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.Order", b =>
                {
                    b.HasOne("PiecykPolHurt.Model.Entities.User", "Buyer")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PiecykPolHurt.Model.Entities.SendPoint", "SendPoint")
                        .WithMany("Orders")
                        .HasForeignKey("SendPointId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("SendPoint");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.OrderLines", b =>
                {
                    b.HasOne("PiecykPolHurt.Model.Entities.Order", "Order")
                        .WithMany("Lines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PiecykPolHurt.Model.Entities.Product", "Product")
                        .WithMany("Lines")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.ProductSendPoint", b =>
                {
                    b.HasOne("PiecykPolHurt.Model.Entities.Product", "Product")
                        .WithMany("SendPoints")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PiecykPolHurt.Model.Entities.SendPoint", "SendPoint")
                        .WithMany("Products")
                        .HasForeignKey("SendPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SendPoint");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.DictionaryType", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.Order", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.Product", b =>
                {
                    b.Navigation("Lines");

                    b.Navigation("SendPoints");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.SendPoint", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PiecykPolHurt.Model.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
