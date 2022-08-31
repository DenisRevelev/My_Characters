﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using My_Characters.Context;

#nullable disable

namespace My_Characters.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220826185105_AddNewModel_RankModel")]
    partial class AddNewModel_RankModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("My_Characters.Models.BiographyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<byte[]>("AvatarImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(52)
                        .HasColumnType("nvarchar(52)");

                    b.Property<string>("Name")
                        .HasMaxLength(52)
                        .HasColumnType("nvarchar(52)");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Biographies");
                });

            modelBuilder.Entity("My_Characters.Models.RankModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("My_Characters.Models.ReferenceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ReferenceImage")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("My_Characters.Models.RenderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RenderImage")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId");

                    b.ToTable("Renders");
                });

            modelBuilder.Entity("My_Characters.Models.SourceFileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId");

                    b.ToTable("SourceFiles");
                });

            modelBuilder.Entity("My_Characters.Models.ToDoListModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<bool>("CheckTask")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Finish")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Task")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("My_Characters.Models.RankModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithMany("RankNavigation")
                        .HasForeignKey("BiographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biography");
                });

            modelBuilder.Entity("My_Characters.Models.ReferenceModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithMany("ReferenceNavigation")
                        .HasForeignKey("BiographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biography");
                });

            modelBuilder.Entity("My_Characters.Models.RenderModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithMany("RenderNavigation")
                        .HasForeignKey("BiographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biography");
                });

            modelBuilder.Entity("My_Characters.Models.SourceFileModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithMany("SourceFileNavigation")
                        .HasForeignKey("BiographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biography");
                });

            modelBuilder.Entity("My_Characters.Models.ToDoListModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithMany("ProgressNavigation")
                        .HasForeignKey("BiographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Biography");
                });

            modelBuilder.Entity("My_Characters.Models.BiographyModel", b =>
                {
                    b.Navigation("ProgressNavigation");

                    b.Navigation("RankNavigation");

                    b.Navigation("ReferenceNavigation");

                    b.Navigation("RenderNavigation");

                    b.Navigation("SourceFileNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
