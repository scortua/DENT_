using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiMessage
        {
            /* Message types */
            public const byte MSG_TYPE_NONE = 0;
            public const byte MSG_TYPE_WINDOW = 1;
            public const byte MSG_TYPE_OBJECT = 2;

            /* -------------------------------------------------------------------------------- */
            /* -- MESSAGE                                                                    -- */
            /* -------------------------------------------------------------------------------- */

            public byte type { get; set; }
            public byte id { get; set; }
            public byte sub_id { get; set; }
            public byte evt { get; set; }
            public uGuiObject src { get; set; }

            public uGuiMessage(byte type = MSG_TYPE_NONE, byte id = 0, byte sub_id = 0, byte evt = 0, uGuiObject src = null)
            {
                this.type = type;
                this.id = id;
                this.sub_id = sub_id;
                this.evt = evt;
                this.src = src;
            }
        }
    }
}
