using Microsoft.EntityFrameworkCore;
using TunaPianoAPI.Models;

namespace TunaPianoAPI.APIs
{
    public class SongAPI
    {
        public static void Map(WebApplication app)
        {
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
        }
    }
}
