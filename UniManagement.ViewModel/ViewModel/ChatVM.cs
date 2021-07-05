using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class ChatVM
    {
        public int ChatId { get; set; }
        public int ChatRoomId { get; set; }
        public string Message { get; set; }
        public long RecieverId { get; set; }
        public long SenderId { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ChatInfoVM
    {
        public int ChatRoomId { get; set; }
        public int ChatRoomName { get; set; }
        public long RecieverId { get; set; }
        public long SenderId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
