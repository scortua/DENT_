using System;
using System.Collections;

namespace uGUI
{
    public class uGuiMessage
    {
        public enum MessageType
        {
            MSG_TYPE_NONE=0,
            MSG_TYPE_WINDOW,
            MSG_TYPE_OBJECT
        }
        public MessageType type { get; set; } = MessageType.MSG_TYPE_NONE;
        public byte id { get; set; } = 0;
        public byte sub_id { get; set; } = 0;
        public byte evt { get; set; } = 0;
        public object src { get; set; } = null;

    }
}
