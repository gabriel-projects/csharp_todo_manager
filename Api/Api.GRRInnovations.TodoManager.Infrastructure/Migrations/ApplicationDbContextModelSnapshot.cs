﻿// <auto-generated />
using System;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.GRRInnovations.TodoManager.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.CategoryModel", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.TaskCategoryModel", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryUid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TaskUid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("CategoryUid");

                    b.HasIndex("TaskUid", "CategoryUid")
                        .IsUnique();

                    b.ToTable("TasksCategories");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.TaskModel", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Recurrent")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserUid")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("UserUid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.UserDetailModel", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserUid")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("UserUid")
                        .IsUnique();

                    b.ToTable("UsersDetails");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.UserModel", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("BlockedAccess")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("PendingConfirm")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("Login")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Login"), new[] { "Password" });

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.TaskCategoryModel", b =>
                {
                    b.HasOne("Api.GRRInnovations.TodoManager.Domain.Entities.CategoryModel", "DbCategory")
                        .WithMany("DbTasksCategories")
                        .HasForeignKey("CategoryUid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Api.GRRInnovations.TodoManager.Domain.Entities.TaskModel", "DbTask")
                        .WithMany("DbTasksCategories")
                        .HasForeignKey("TaskUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbCategory");

                    b.Navigation("DbTask");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.TaskModel", b =>
                {
                    b.HasOne("Api.GRRInnovations.TodoManager.Domain.Entities.UserModel", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.UserDetailModel", b =>
                {
                    b.HasOne("Api.GRRInnovations.TodoManager.Domain.Entities.UserModel", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("Api.GRRInnovations.TodoManager.Domain.Entities.UserDetailModel", "UserUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.CategoryModel", b =>
                {
                    b.Navigation("DbTasksCategories");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.TaskModel", b =>
                {
                    b.Navigation("DbTasksCategories");
                });

            modelBuilder.Entity("Api.GRRInnovations.TodoManager.Domain.Entities.UserModel", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("UserDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
