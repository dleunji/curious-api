using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Recommendations = new HashSet<Recommendation>();
        }

        public int AnswerId { get; set; }
        public int? MemberId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? DeletedYn { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
    }
}
