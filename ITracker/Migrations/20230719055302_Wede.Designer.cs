﻿// <auto-generated />
using System;
using InitiativeTracker.DataBaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITracker.Migrations
{
    [DbContext(typeof(DatabaseAccess))]
    [Migration("20230719055302_Wede")]
    partial class Wede
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ITracker.Models.Contributor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ideaId")
                        .HasColumnType("int");

                    b.Property<int>("taskId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ideaId");

                    b.ToTable("contributorTable");
                });

            modelBuilder.Entity("ITracker.Models.TaskApprovers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdOfApprover")
                        .HasColumnType("int");

                    b.Property<int>("IdOfTask")
                        .HasColumnType("int");

                    b.Property<int>("approverId")
                        .HasColumnType("int");

                    b.Property<string>("feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("taskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdOfApprover");

                    b.HasIndex("IdOfTask");

                    b.ToTable("taskApproversTable");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Approver", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("approversTable");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Comments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentsDateOnly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentsTimeOnly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdOfIdea")
                        .HasColumnType("int");

                    b.Property<int>("Owner")
                        .HasColumnType("int");

                    b.Property<int>("Taskid")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("IdOfIdea");

                    b.HasIndex("Owner");

                    b.ToTable("commentsTable");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdOFUser")
                        .HasColumnType("int");

                    b.Property<int?>("approverId")
                        .HasColumnType("int");

                    b.Property<string>("endDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idOfOwner")
                        .HasColumnType("int");

                    b.Property<string>("ideaCreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("isDelete")
                        .HasColumnType("int");

                    b.Property<int>("like")
                        .HasColumnType("int");

                    b.Property<string>("longDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("signOff")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("startDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdOFUser");

                    b.ToTable("ideaTable");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("rolesTable");
                });

            modelBuilder.Entity("InitiativeTracker.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("JWT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("blood_grop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("linkedin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rId")
                        .HasColumnType("int");

                    b.Property<int?>("rating")
                        .HasColumnType("int");

                    b.Property<string>("secondary_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("UserType");

                    b.ToTable("usersTable");
                });

            modelBuilder.Entity("ITracker.Models.Contributor", b =>
                {
                    b.HasOne("InitiativeTracker.Models.Idea", null)
                        .WithMany("contributors")
                        .HasForeignKey("ideaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITracker.Models.TaskApprovers", b =>
                {
                    b.HasOne("InitiativeTracker.Models.User", "approver")
                        .WithMany()
                        .HasForeignKey("IdOfApprover")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InitiativeTracker.Models.Idea", "idea")
                        .WithMany()
                        .HasForeignKey("IdOfTask")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("approver");

                    b.Navigation("idea");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Approver", b =>
                {
                    b.HasOne("InitiativeTracker.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userid");

                    b.Navigation("user");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Comments", b =>
                {
                    b.HasOne("InitiativeTracker.Models.Idea", "Idea")
                        .WithMany()
                        .HasForeignKey("IdOfIdea")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InitiativeTracker.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("Owner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Idea");

                    b.Navigation("user");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Idea", b =>
                {
                    b.HasOne("InitiativeTracker.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdOFUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InitiativeTracker.Models.User", b =>
                {
                    b.HasOne("InitiativeTracker.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("UserType");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("InitiativeTracker.Models.Idea", b =>
                {
                    b.Navigation("contributors");
                });
#pragma warning restore 612, 618
        }
    }
}
