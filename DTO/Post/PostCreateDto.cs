using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Post
{
    public class PostCreateDto
    {
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }

}
