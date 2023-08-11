﻿// <auto-generated />
using System;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User.Value_objects;
using Helper.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Helper.Infrastructure.Migrations
{
    [DbContext(typeof(HelperDbContext))]
    partial class HelperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Helper.Core.Inquiry.Inquiry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Status>("AcceptanceStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeasibilityNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Variants>("SolutionDecision")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("Helper.Core.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Roles>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Helper.Core.Inquiry.Inquiry", b =>
                {
                    b.HasOne("Helper.Core.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Helper.Core.Inquiry.ValueObjects.RealisationDate", "RequestedCompletionDate", b1 =>
                        {
                            b1.Property<Guid>("InquiryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("End")
                                .HasColumnType("datetime2")
                                .HasColumnName("RequestedEndDate");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2")
                                .HasColumnName("RequestedStartDate");

                            b1.HasKey("InquiryId");

                            b1.ToTable("Inquiries");

                            b1.WithOwner()
                                .HasForeignKey("InquiryId");
                        });

                    b.Navigation("Author");

                    b.Navigation("RequestedCompletionDate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
