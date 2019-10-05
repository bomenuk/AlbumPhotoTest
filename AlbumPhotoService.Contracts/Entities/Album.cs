using System.Collections.Generic;

namespace AlbumPhotoService.Contracts.Entities
{
    public class Album: baseServiceEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public IList<Photo> Photos { get; set; }
    }
}
