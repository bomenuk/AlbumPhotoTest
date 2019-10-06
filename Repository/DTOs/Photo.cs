﻿namespace Repository.Contracts.DTOs
{
    public class Photo:baseEntity
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
