using DevExpress.Xpo;
using System;

namespace board.Request
{
    public class QuestionRequest
    {
        public int MemberId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }
    }
}