using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Sympathy
    {
        public int SympathyId { get; set; }
        public int? QuestionId { get; set; }
        public int? MemberId { get; set; }
        public bool? DeletedYn { get; set; }

        public virtual Member Member { get; set; }
        public virtual Question Question { get; set; }
    }
}
