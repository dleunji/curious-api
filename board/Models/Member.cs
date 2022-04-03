using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.Xpo;

#nullable disable

namespace board.Models
{
    public partial class Member
    {
        public Member()
        {
            Answers = new HashSet<Answer>();
            AttachedFiles = new HashSet<AttachedFile>();
            Comments = new HashSet<Comment>();
            Notifications = new HashSet<Notification>();
            Questions = new HashSet<Question>();
            Recommendations = new HashSet<Recommendation>();
            Sympathies = new HashSet<Sympathy>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        
        public string MemberPassword { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Introduction { get; set; }
        public string MailAddress { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<AttachedFile> AttachedFiles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public virtual ICollection<Sympathy> Sympathies { get; set; }
    }
}
