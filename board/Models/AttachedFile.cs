using System;
using System.Collections.Generic;

#nullable disable

namespace board.Models
{
    public partial class AttachedFile
    {
        public int AttacehdfileId { get; set; }
        public int? PostId { get; set; }
        public string PostType { get; set; }
        public int? MemberId { get; set; }
        public string OriginFileName { get; set; }
        public string OriginFilePath { get; set; }
        public string SaveFileName { get; set; }
        public string SaveFilePath { get; set; }
        public int? FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? DeletedYn { get; set; }

        public virtual Member Member { get; set; }
    }
}
