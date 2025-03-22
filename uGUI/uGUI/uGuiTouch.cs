using System;

namespace uGUI
{
    public class uGuiTouch
    {
        public enum TouchState
        {
            TOUCH_STATE_RELEASED = 0,
            TOUCH_STATE_PRESSED
        }
        public TouchState state { get; set; } = TouchState.TOUCH_STATE_RELEASED;
        public Int16 xp { get; set; } = 0;
        public Int16 yp { get; set; } = 0;
    }
}
