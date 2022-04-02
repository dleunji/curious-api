using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace board.Request
{
    public class CommentRequest
    {
        public int MemberId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public string PostType { get; set; }
    }
}
