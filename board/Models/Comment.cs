using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentComment = new HashSet<Comment>();
        }

        public int CommentId { get; set; }
        public int? MemberId { get; set; }
        public int? PostId { get; set; }
        public string PostType { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? DeletedYn { get; set; }
        public int? ParentCommentId { get; set; }
        public int? Depth { get; set; }

        public virtual Member Member { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
