using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Comment
{
    public class CommentUpdateDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = null!;

    }
}
