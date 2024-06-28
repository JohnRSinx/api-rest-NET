﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dataContext;

#nullable disable

namespace dotnet.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("entities.TaskEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_create");

                    b.Property<DateTime>("DateUpgrade")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_upgrade");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_create");

                    b.Property<DateTime>("DateUpgrade")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_upgrade");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("entities.TaskEntity", b =>
                {
                    b.HasOne("entities.UserEntity", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("entities.UserEntity", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}