
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TunaPianoAPI.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TunaPianoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // allows passing datetimes without time zone data 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // allows our api endpoints to access the database through Entity Framework Core
            builder.Services.AddNpgsql<TunaPianoDbContext>(builder.Configuration["TunaPianoDbConnectionString"]);

            // Set the JSON serializer options
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Artist
            // View List of Artists
            app.MapGet("/artists", (TunaPianoDbContext db) =>
            {
                return db.Artists.ToList();
            });

            // View Specific Artist and their songs
            app.MapGet("/artists/{artistId}", (TunaPianoDbContext db, int artistId) =>
            {
                var artist = db.Artists
                                .Include(p => p.Songs)
                                .SingleOrDefault(u => u.ArtistId == artistId);

                if (artist == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(artist);

            });

            // Create Artist
            app.MapPost("/artists", (TunaPianoDbContext db, Artist artist) =>
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return Results.Created($"/artist/{artist.ArtistId}", artist);
            });

            // Update Artist
            app.MapPut("/artists/{artistId}", (TunaPianoDbContext db, int ArtistId, Artist artist) =>
            {
                Artist artistToUpdate = db.Artists.SingleOrDefault(artist => artist.ArtistId == ArtistId);
                if (artistToUpdate == null)
                {
                    return Results.NotFound();
                }
                artistToUpdate.Name = artist.Name;
                artistToUpdate.Age = artist.Age;
                artistToUpdate.Bio = artist.Bio;

                db.SaveChanges();
                return Results.NoContent();
            });

            // Delete Artist
            app.MapDelete("/artists/{artistId}", (TunaPianoDbContext db, int ArtistId) =>
            {
                Artist artist = db.Artists.SingleOrDefault(artist => artist.ArtistId == ArtistId);
                if (artist == null)
                {
                    return Results.NotFound();
                }
                db.Artists.Remove(artist);
                db.SaveChanges();
                return Results.NoContent();

            });

            // Song
            // View List of Songs
            app.MapGet("/songs", (TunaPianoDbContext db) =>
            {
                return db.Songs.ToList();
            });

            // View Song Details
            app.MapGet("/songs/{songId}", (TunaPianoDbContext db, int songId) =>
            {
                var song = db.Songs
                .Include(s => s.Genres)
                                    
                .Include(s => s.Artist)
                                    
                .SingleOrDefault(s => s.SongId == songId);

                if (song == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(song);

            });

            //Create a song
            app.MapPost("/songs", (TunaPianoDbContext db, Song song) =>
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return Results.Created($"/songs/{song.SongId}", song);
            });

            //Update a song
            app.MapPut("/songs/{songId}", (TunaPianoDbContext db, int SongId, Song song) =>
            {
                Song songToUpdate = db.Songs.SingleOrDefault(song => song.SongId == SongId);
                if (songToUpdate == null)
                {
                    return Results.NotFound();
                }
                songToUpdate.Title = song.Title;
                songToUpdate.ArtistId = song.ArtistId;
                songToUpdate.Album = song.Album;
                songToUpdate.Length = song.Length;


                db.SaveChanges();
                return Results.NoContent();
            });

            //Delete a song
            app.MapDelete("/songs/{songId}", (TunaPianoDbContext db, int SongId) =>
            {
                Song song = db.Songs.SingleOrDefault(song => song.SongId == SongId);
                if (song == null)
                {
                    return Results.NotFound();
                }
                db.Songs.Remove(song);
                db.SaveChanges();
                return Results.NoContent();

            });

            //Genre 
            //View list of genres
            app.MapGet("/genres", (TunaPianoDbContext db) =>
            {
                return db.Genres.ToList();
            });

            //View genre details
            app.MapGet("/genres/{genreId}", (TunaPianoDbContext db, int genreId) =>
            {
                var genre = db.Genres
                .Include(s => s.Songs)

                .SingleOrDefault(s => s.GenreId == genreId);

                if (genre == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(genre);

            });
            app.Run();
        }
    }
}
