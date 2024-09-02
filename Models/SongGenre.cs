namespace TunaPianoAPI.Models
{
    public class SongGenre
    {
        public int Id { get; set; }
        public Song? Song { get; set; }

        public Genre? Genre { get; set; }

    }
}
