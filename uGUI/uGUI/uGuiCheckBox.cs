using System;
using System.Collections;

namespace uGUI
{
    public class uGuiCheckBox
    {
        // checkbox state
        public const byte CHB_STATE_RELEASED = (0 << 0);
        public const byte CHB_STATE_PRESSED = (1 << 0);
        public const byte CHB_STATE_ALWAYS_REDRAW = (1 << 1);
        // checkbox style
        public const byte CHB_STYLE_2D = (0 << 0);
        public const byte CHB_STYLE_3D = (1 << 0);
        public const byte CHB_STYLE_TOGGLE_COLORS = (1 << 1);
        public const byte CHB_STYLE_USE_ALTERNATE_COLORS = (1 << 2);
        public const byte CHB_STYLE_NO_BORDERS = (1 << 3);
        public const byte CHB_STYLE_NO_FILL = (1 << 4);
        // checkbox event
        public const byte CHB_EVENT_CLICKED = (byte)uGuiObject.ObjectEvents.OBJ_EVENT_CLICKED;
        // checkbox
        public byte state { get; set; } = 0;
        public byte style { get; set; } = 0;
        public uGuiColor fc { get; set; } = null;
        public uGuiColor bc { get; set; } = null;
        public uGuiColor afc { get; set; } = null;
        public uGuiColor abc { get; set; } = null;
        public uGuiFont font { get; set; } = null;
        public byte align { get; set; } = 0;
        public sbyte h_space { get; set; } = 0;
        public sbyte v_space { get; set; } = 0;
        public string str { get; set; } = null;
        public bool check { get; set; } = false;
    }
}

