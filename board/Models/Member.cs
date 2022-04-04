using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Xpo;

#nullable disable

namespace board.Models
{
    public partial class Member
    {
        public Member()
        {
            Comments = new HashSet<Comment>();
            Questions = new HashSet<Question>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberPassword { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Introduction { get; set; }
        public string MailAddress { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
