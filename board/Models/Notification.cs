using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? MemberId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? CheckedAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
