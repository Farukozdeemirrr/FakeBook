using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Comment
{
    public class CommentDto
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string? UserProfilePicture { get; set; }
    }

}
