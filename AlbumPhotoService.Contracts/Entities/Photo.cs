namespace AlbumPhotoService.Contracts.Entities
{
    public class Photo: baseServiceEntity
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
