using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Recommendation
    {
        public int RecommendationId { get; set; }
        public int? AnswerId { get; set; }
        public int? MemberId { get; set; }
        public bool? IsPositive { get; set; }
        public bool? DeletedYn { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Member Member { get; set; }
    }
}
