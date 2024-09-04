using Microsoft.EntityFrameworkCore;
using TunaPianoAPI.Models;

namespace TunaPianoAPI
{
    public class TunaPianoDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().HasData(new Artist[]
                {
                    new Artist
                    {
                         ArtistId = 1,
                         Name = "Elton John",
                         Age = 76,
                         Bio = "British singer, pianist, and composer, known for his numerous hits.",

                    },
                    new Artist
                    {
                        ArtistId = 2,
                        Name = "Ed Sheeran",
                        Age = 33,
                        Bio = "A British singer-songwriter known for his soulful voice, acoustic sound"

                    },
                    new Artist
                    {
                        ArtistId = 3,
                        Name = "Johnny Cash",
                        Age = 92,
                        Bio = " American singer-songwriter renowned for his deep, resonant voice, and rebellious image"

                    }

                });
            modelBuilder.Entity<Song>().HasData(new Song[]
            {
                new Song
                {
                        SongId = 1,
                        Title = "Rocket Man",
                        ArtistId = 1,
                        Album = "Honky Château",
                        Length = 4.41m,

                },
                new Song
                {
                        SongId = 2,
                        Title = "Perfect",
                        ArtistId = 2,
                        Album = "Divide",
                        Length = 4.23m,

                },
                new Song
                {
                        SongId = 3,
                        Title = "Ring of Fire",
                        ArtistId = 3,
                        Album = "The Best of Johnny Cash",
                        Length = 2.38m,

                }

            });
            modelBuilder.Entity<Genre>().HasData(new Genre[]
            {
                new Genre
                {
                    GenreId = 1,
                    Description = "Rock"
                },
                new Genre
                {
                    GenreId = 2,
                    Description = "Pop"
                },
                new Genre
                {
                    GenreId = 3,
                    Description = "Country"
                },


            });

            modelBuilder.Entity<Song>()
                .HasMany(s => s.Genres)
                .WithMany(g => g.Songs)
                .UsingEntity(j => j.HasData(
                    new { SongsSongId = 1, GenresGenreId = 1 },
                    new { SongsSongId = 2, GenresGenreId = 2 },
                    new { SongsSongId = 3, GenresGenreId = 3 }
                ));

        }

        public TunaPianoDbContext(DbContextOptions<TunaPianoDbContext> context) : base(context)
        {

        }


    }
}
