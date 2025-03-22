using System;

namespace uGUI
{
    public class uGuiWindow
    {
        public delegate void WindowCallBackDelegate(uGuiMessage msg);
        // windows states
        public const byte WND_STATE_FREE = (1 << 0);
        public const byte WND_STATE_VALID = (1 << 1);
        public const byte WND_STATE_BUSY = (1 << 2);
        public const byte WND_STATE_VISIBLE = (1 << 3);
        public const byte WND_STATE_ENABLE = (1 << 4);
        public const byte WND_STATE_UPDATE = (1 << 5);
        public const byte WND_STATE_REDRAW_TITLE = (1 << 6);
        // windows style
        public const byte WND_STYLE_2D = (0 << 0);
        public const byte WND_STYLE_3D = (1 << 0);
        public const byte WND_STYLE_HYDE_TITLE = (0 << 1);
        public const byte WND_STYLE_SHOW_TITLE = (1 << 1);
        // windows
        public ArrayList objlst { get; set; } = new ArrayList();
        public byte state { get; set; } = 0;
        public uGuiColor fc { get; set; } = null;
        public uGuiColor bc { get; set; } = null;
        public Int16 xs { get; set; } = 0;
        public Int16 ys { get; set; } = 0;
        public Int16 xe { get; set; } = 0;
        public Int16 ye { get; set; } = 0;
        public byte style { get; set; } = 0;
        public uGuiTitle title { get; set; } = null;
        public event WindowCallBackDelegate WindowCallBack = null;
    }
}
