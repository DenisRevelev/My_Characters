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
    [Migration("20220629090441_CreateDatabase")]
    partial class CreateDatabase
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

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Biographies");
                });

            modelBuilder.Entity("My_Characters.Models.ProgressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiographyId")
                        .HasColumnType("int");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<bool>("StatusProgress")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BiographyId")
                        .IsUnique();

                    b.ToTable("Progresses");
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

            modelBuilder.Entity("My_Characters.Models.ProgressModel", b =>
                {
                    b.HasOne("My_Characters.Models.BiographyModel", "Biography")
                        .WithOne("ProgressNavigation")
                        .HasForeignKey("My_Characters.Models.ProgressModel", "BiographyId")
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

            modelBuilder.Entity("My_Characters.Models.BiographyModel", b =>
                {
                    b.Navigation("ProgressNavigation");

                    b.Navigation("ReferenceNavigation");

                    b.Navigation("RenderNavigation");

                    b.Navigation("SourceFileNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
