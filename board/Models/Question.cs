using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Question
    {
        public Question()
        {
            Sympathies = new HashSet<Sympathy>();
        }

        public int QuestionId { get; set; }
        public int? MemberId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? DeletedYn { get; set; }
        public int? ViewedCnt { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<Sympathy> Sympathies { get; set; }
    }
}
