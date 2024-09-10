using Microsoft.EntityFrameworkCore;
using TunaPianoAPI.Models;

namespace TunaPianoAPI.APIs
{
    public class GenreAPI
    {
        public static void Map(WebApplication app)
        {
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

            //Create genre
            app.MapPost("/genres", (TunaPianoDbContext db, Genre genre) =>
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return Results.Created($"/genres/{genre.GenreId}", genre);
            });

            //Update genre
            app.MapPut("/genres/{genreId}", (TunaPianoDbContext db, int GenreId, Genre genre) =>
            {
                Genre genreToUpdate = db.Genres.SingleOrDefault(genre => genre.GenreId == GenreId);
                if (genreToUpdate == null)
                {
                    return Results.NotFound();
                }
                genreToUpdate.Description = genre.Description;


                db.SaveChanges();
                return Results.NoContent();
            });

            //Delete genre
            app.MapDelete("/genres/{genreId}", (TunaPianoDbContext db, int GenreId) =>
            {
                Genre genre = db.Genres.SingleOrDefault(genre => genre.GenreId == GenreId);
                if (genre == null)
                {
                    return Results.NotFound();
                }
                db.Genres.Remove(genre);
                db.SaveChanges();
                return Results.NoContent();

            });
        }
    }
}
