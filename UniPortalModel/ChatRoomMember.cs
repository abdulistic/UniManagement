using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class ChatRoomMember
    {
        public int MemberId { get; set; }
        public int ChatRoomId { get; set; }
        public long MemberUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
