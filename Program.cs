
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TunaPianoAPI.Models;
using System.Runtime.CompilerServices;

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

            app.Run();
        }
    }
}
