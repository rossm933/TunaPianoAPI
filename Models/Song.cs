namespace TunaPianoAPI.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string? Title { get; set; }
        public int ArtistId { get; set; }
        public string? Album { get; set; }
        public decimal? Length { get; set; }

        public Artist? Artist { get; set; } 
        public List<Genre>? Genres { get; set;
    }
}
