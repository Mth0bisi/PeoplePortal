﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperHeroAPI.Data;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SuperHeroAPI.Models.Account", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("account_number");

                    b.Property<decimal>("OutstandingBalance")
                        .HasColumnType("money")
                        .HasColumnName("outstanding_balance");

                    b.Property<int>("PersonCode")
                        .HasColumnType("int")
                        .HasColumnName("person_code");

                    b.HasKey("Code");

                    b.HasIndex("PersonCode");

                    b.HasIndex(new[] { "AccountNumber" }, "IX_Account_num")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Person", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"), 1L, 1);

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id_number");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("surname");

                    b.HasKey("Code");

                    b.HasIndex(new[] { "IdNumber" }, "IX_Person_id")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Status", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"), 1L, 1);

                    b.Property<int>("AccountCode")
                        .HasColumnType("int");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Transaction", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"), 1L, 1);

                    b.Property<int>("AccountCode")
                        .HasColumnType("int")
                        .HasColumnName("account_code");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CaptureDate")
                        .HasColumnType("datetime")
                        .HasColumnName("capture_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime")
                        .HasColumnName("transaction_date");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.HasIndex("AccountCode");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Account", b =>
                {
                    b.HasOne("SuperHeroAPI.Models.Person", "PersonCodeNavigation")
                        .WithMany("Accounts")
                        .HasForeignKey("PersonCode")
                        .IsRequired()
                        .HasConstraintName("FK_Account_Person");

                    b.Navigation("PersonCodeNavigation");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Transaction", b =>
                {
                    b.HasOne("SuperHeroAPI.Models.Account", "AccountCodeNavigation")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountCode")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_Account");

                    b.Navigation("AccountCodeNavigation");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("SuperHeroAPI.Models.Person", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}