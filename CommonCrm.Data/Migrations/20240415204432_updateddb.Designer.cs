﻿// <auto-generated />
using System;
using CommonCrm.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommonCrm.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240415204432_updateddb")]
    partial class updateddb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("CollectionProduct", b =>
                {
                    b.Property<int>("CollectionsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("CollectionsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CollectionProduct");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.AppUser.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("GenderText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCustomerCompany")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCustomerPerson")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxOffice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TcNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.CategoryProduct", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoryProducts");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.CollectionProduct", b =>
                {
                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CollectionId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CollectionProducts");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("CurrencyDollar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrencyEuro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Depth")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDolarDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEuroDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTlDefault")
                        .HasColumnType("bit");

                    b.Property<int?>("KdvDolar")
                        .HasColumnType("int");

                    b.Property<int?>("KdvEuro")
                        .HasColumnType("int");

                    b.Property<int?>("KdvTL")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfferDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfferQuantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Packet")
                        .HasColumnType("int");

                    b.Property<decimal?>("SalesPriceDolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SalesPriceEuro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SalesPriceTL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalDolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalEuro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalTL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.Property<int?>("Volume")
                        .HasColumnType("int");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.ProductUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ProductsUnit");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonCrm.Data.Entities.Product.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectionProduct", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonCrm.Data.Entities.Product.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Attribute", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.CategoryProduct", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonCrm.Data.Entities.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.CollectionProduct", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.Collection", "Collection")
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonCrm.Data.Entities.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CommonCrm.Data.Entities.Product.Product", b =>
                {
                    b.HasOne("CommonCrm.Data.Entities.Product.ProductUnit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });
#pragma warning restore 612, 618
        }
    }
}
