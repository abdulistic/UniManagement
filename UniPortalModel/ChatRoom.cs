using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public string ChatRoomName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
