using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiTouch
        {
            public const byte TOUCH_STATE_PRESSED = 1;
            public const byte TOUCH_STATE_RELEASED = 0;

            /* -------------------------------------------------------------------------------- */
            /* -- TOUCH                                                                      -- */
            /* -------------------------------------------------------------------------------- */

            public byte state { get; set; }
            public Int16 xp { get; set; }
            public Int16 yp { get; set; }

            public uGuiTouch(Int16 x = -1, Int16 y = -1, byte state = TOUCH_STATE_RELEASED)
            {
                this.state = state;
                this.xp = x;
                this.yp = y;
            }
        }
    }
}
