using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public int ChatRoomId { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
