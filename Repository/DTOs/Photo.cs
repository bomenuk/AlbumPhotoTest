using Repository.Contracts.DTOs;

namespace Repository.DTOs
{
    public class Photo:baseEntity
    {
        public int AlbumnId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
