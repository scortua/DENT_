using System;
using System.Collections;

namespace uGUI
{
    public class uGuiObject
    {
        public delegate void ObjectUpdateDelegate(uGuiWindow wnd, uGuiObject obj);
        public enum ObjectType : byte
        {
            OBJ_TYPE_NONE = 0,
            OBJ_TYPE_BUTTON,
            OBJ_TYPE_TEXTBOX,
            OBJ_TYPE_IMAGE,
            OBJ_TYPE_CHECKBOX
        }

        public enum ObjectEvents : byte
        {
            OBJ_EVENT_NONE = 0,
            OBJ_EVENT_CLICKED,
            OBJ_EVENT_PRERENDER,
            OBJ_EVENT_POSTRENDER,
            OBJ_EVENT_PRESSED,
            OBJ_EVENT_RELEASED,
        }
        // object states
        public const byte OBJ_STATE_FREE = (1 << 0);
        public const byte OBJ_STATE_VALID = (1 << 1);
        public const byte OBJ_STATE_BUSY = (1 << 2);
        public const byte OBJ_STATE_VISIBLE = (1 << 3);
        public const byte OBJ_STATE_ENABLE = (1 << 4);
        public const byte OBJ_STATE_UPDATE = (1 << 5);
        public const byte OBJ_STATE_REDRAW = (1 << 6);
        public const byte OBJ_STATE_TOUCH_ENABLE = (1 << 7);
        public const byte OBJ_STATE_INIT = (OBJ_STATE_FREE | OBJ_STATE_VALID);
        // object touch states
        public const byte OBJ_TOUCH_STATE_CHANGED = (1 << 0);
        public const byte OBJ_TOUCH_STATE_PRESSED_ON_OBJECT = (1 << 1);
        public const byte OBJ_TOUCH_STATE_PRESSED_OUTSIDE_OBJECT = (1 << 2);
        public const byte OBJ_TOUCH_STATE_RELEASED_ON_OBJECT = (1 << 3);
        public const byte OBJ_TOUCH_STATE_RELEASED_OUTSIDE_OBJECT = (1 << 4);
        public const byte OBJ_TOUCH_STATE_IS_PRESSED_ON_OBJECT = (1 << 5);
        public const byte OBJ_TOUCH_STATE_IS_PRESSED = (1 << 6);
        public const byte OBJ_TOUCH_STATE_CLICK_ON_OBJECT = (1 << 7);
        public const byte OBJ_TOUCH_STATE_INIT = 0;
        // object
        public byte state { get; set; } = ObjectStates.OBJ_STATE_INIT;
        public byte touch_state { get; set; } = ObjectTouchStates.OBJ_TOUCH_STATE_INIt;
        public event ObjectUpdateDelegate ObjectUpdate = null;
        public uGui_Area a_abs { get; set; } = null;
        public uGui_Area a_rel { get; set; } = null;
        public byte type { get; set; } = 0;
        public ushort id { get; set; } = 0;
        public byte evt { get; set; } = 0;
        public object data { get; set; } = null;
    }
}
