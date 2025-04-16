using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Post
{
    public class PostUpdateDto
    {
        public long Id { get; set; }
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }    
       
    }
}
