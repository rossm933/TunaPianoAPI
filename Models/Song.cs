﻿namespace TunaPianoAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Artist? Artist { get; set; }
        public string? Album { get; set; }
        public decimal? Length { get; set; }
    }
}
