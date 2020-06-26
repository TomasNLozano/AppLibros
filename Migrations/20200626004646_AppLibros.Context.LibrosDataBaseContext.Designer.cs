﻿// <auto-generated />
using System;
using AppLibros.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppLibros.Migrations
{
    [DbContext(typeof(LibrosDataBaseContext))]
    [Migration("20200626004646_AppLibros.Context.LibrosDataBaseContext")]
    partial class AppLibrosContextLibrosDataBaseContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppLibros.Models.Autor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Usuarioid")
                        .HasColumnType("int");

                    b.Property<string>("apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Usuarioid");

                    b.ToTable("autores");
                });

            modelBuilder.Entity("AppLibros.Models.AutoresFavoritos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idAutor")
                        .HasColumnType("int");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("autoresFavoritos");
                });

            modelBuilder.Entity("AppLibros.Models.Libro", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Usuarioid")
                        .HasColumnType("int");

                    b.Property<int?>("autorid")
                        .HasColumnType("int");

                    b.Property<string>("isbn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("puntaje")
                        .HasColumnType("float");

                    b.Property<string>("resena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("votos")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Usuarioid");

                    b.HasIndex("autorid");

                    b.ToTable("libros");
                });

            modelBuilder.Entity("AppLibros.Models.LibrosFavoritos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idLibro")
                        .HasColumnType("int");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("librosFavoritos");
                });

            modelBuilder.Entity("AppLibros.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("edad")
                        .HasColumnType("int");

                    b.Property<bool>("esAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("AppLibros.Models.Autor", b =>
                {
                    b.HasOne("AppLibros.Models.Usuario", null)
                        .WithMany("autoresFavoritos")
                        .HasForeignKey("Usuarioid");
                });

            modelBuilder.Entity("AppLibros.Models.Libro", b =>
                {
                    b.HasOne("AppLibros.Models.Usuario", null)
                        .WithMany("librosFavoritos")
                        .HasForeignKey("Usuarioid");

                    b.HasOne("AppLibros.Models.Autor", "autor")
                        .WithMany("libros")
                        .HasForeignKey("autorid");
                });
#pragma warning restore 612, 618
        }
    }
}