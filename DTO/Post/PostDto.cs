using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Post
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string? UserProfilePicture { get; set; }
    }

}
