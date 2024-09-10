using TunaPianoAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TunaPianoAPI.APIs
{
    public class ArtistAPI
    {
        public static void Map(WebApplication app)
        {
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
        }
    }
}
