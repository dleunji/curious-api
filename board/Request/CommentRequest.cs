﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace board.Request
{
    public class CommentRequest
    {
        public int MemberId { get; set; }

        public int QuestionId { get; set; }

        public string Content { get; set; }
    }
}
