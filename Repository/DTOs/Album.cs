using Repository.Contracts.DTOs;

namespace Repository.DTOs
{
    public class Album:baseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
