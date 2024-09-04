﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TunaPianoAPI;

#nullable disable

namespace TunaPianoAPI.Migrations
{
    [DbContext(typeof(TunaPianoDbContext))]
    partial class TunaPianoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("integer");

                    b.Property<int>("SongsSongId")
                        .HasColumnType("integer");

                    b.HasKey("GenresGenreId", "SongsSongId");

                    b.HasIndex("SongsSongId");

                    b.ToTable("GenreSong");

                    b.HasData(
                        new
                        {
                            GenresGenreId = 1,
                            SongsSongId = 1
                        },
                        new
                        {
                            GenresGenreId = 2,
                            SongsSongId = 2
                        },
                        new
                        {
                            GenresGenreId = 3,
                            SongsSongId = 3
                        });
                });

            modelBuilder.Entity("TunaPianoAPI.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ArtistId"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            ArtistId = 1,
                            Age = 76,
                            Bio = "British singer, pianist, and composer, known for his numerous hits.",
                            Name = "Elton John"
                        },
                        new
                        {
                            ArtistId = 2,
                            Age = 33,
                            Bio = "A British singer-songwriter known for his soulful voice, acoustic sound",
                            Name = "Ed Sheeran"
                        },
                        new
                        {
                            ArtistId = 3,
                            Age = 92,
                            Bio = " American singer-songwriter renowned for his deep, resonant voice, and rebellious image",
                            Name = "Johnny Cash"
                        });
                });

            modelBuilder.Entity("TunaPianoAPI.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Description = "Rock"
                        },
                        new
                        {
                            GenreId = 2,
                            Description = "Pop"
                        },
                        new
                        {
                            GenreId = 3,
                            Description = "Country"
                        });
                });

            modelBuilder.Entity("TunaPianoAPI.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SongId"));

                    b.Property<string>("Album")
                        .HasColumnType("text");

                    b.Property<int>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Length")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("SongId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            SongId = 1,
                            Album = "Honky Château",
                            ArtistId = 1,
                            Length = 4.41m,
                            Title = "Rocket Man"
                        },
                        new
                        {
                            SongId = 2,
                            Album = "Divide",
                            ArtistId = 2,
                            Length = 4.23m,
                            Title = "Perfect"
                        },
                        new
                        {
                            SongId = 3,
                            Album = "The Best of Johnny Cash",
                            ArtistId = 3,
                            Length = 2.38m,
                            Title = "Ring of Fire"
                        });
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("TunaPianoAPI.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TunaPianoAPI.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsSongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TunaPianoAPI.Models.Song", b =>
                {
                    b.HasOne("TunaPianoAPI.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("TunaPianoAPI.Models.Artist", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
