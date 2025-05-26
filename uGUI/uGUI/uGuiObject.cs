using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiObject
        {
            public delegate void ObjectUpdateDelegate ();
            
            /* Default IDs */
            public const byte OBJ_ID_0 = 0;
            public const byte OBJ_ID_1 = 1;
            public const byte OBJ_ID_2 = 2;
            public const byte OBJ_ID_3 = 3;
            public const byte OBJ_ID_4 = 4;
            public const byte OBJ_ID_5 = 5;
            public const byte OBJ_ID_6 = 6;
            public const byte OBJ_ID_7 = 7;
            public const byte OBJ_ID_8 = 8;
            public const byte OBJ_ID_9 = 9;
            public const byte OBJ_ID_10 = 10;
            public const byte OBJ_ID_11 = 11;
            public const byte OBJ_ID_12 = 12;
            public const byte OBJ_ID_13 = 13;
            public const byte OBJ_ID_14 = 14;
            public const byte OBJ_ID_15 = 15;
            public const byte OBJ_ID_16 = 16;
            public const byte OBJ_ID_17 = 17;
            public const byte OBJ_ID_18 = 18;
            public const byte OBJ_ID_19 = 19;
            public const byte OBJ_ID_20 = 20;
            public const byte OBJ_ID_21 = 21;
            public const byte OBJ_ID_22 = 22;
            public const byte OBJ_ID_23 = 23;
            public const byte OBJ_ID_24 = 24;
            public const byte OBJ_ID_25 = 25;
            public const byte OBJ_ID_26 = 26;
            public const byte OBJ_ID_27 = 27;
            public const byte OBJ_ID_28 = 28;
            public const byte OBJ_ID_29 = 29;
            public const byte OBJ_ID_30 = 30;
            public const byte OBJ_ID_31 = 31;
            public const byte OBJ_ID_32 = 32;
            public const byte OBJ_ID_33 = 33;
            public const byte OBJ_ID_34 = 34;
            public const byte OBJ_ID_35 = 35;
            public const byte OBJ_ID_36 = 36;
            public const byte OBJ_ID_37 = 37;
            public const byte OBJ_ID_38 = 38;
            public const byte OBJ_ID_39 = 39;
            public const byte OBJ_ID_40 = 40;
            public const byte OBJ_ID_41 = 41;
            public const byte OBJ_ID_42 = 42;
            public const byte OBJ_ID_43 = 43;
            public const byte OBJ_ID_44 = 44;
            public const byte OBJ_ID_45 = 45;
            public const byte OBJ_ID_46 = 46;
            public const byte OBJ_ID_47 = 47;
            public const byte OBJ_ID_48 = 48;
            public const byte OBJ_ID_49 = 49;
            public const byte OBJ_ID_50 = 50;

            /* Currently supported objects */
            public const byte OBJ_TYPE_NONE = 0;
            public const byte OBJ_TYPE_BUTTON = 1;
            public const byte OBJ_TYPE_TEXTBOX = 2;
            public const byte OBJ_TYPE_IMAGE = 3;
            public const byte OBJ_TYPE_CHECKBOX = 4;
            public const byte OBJ_TYPE_LISTBOX = 5;

            /* Standard object events */
            public const byte OBJ_EVENT_NONE = 0;
            public const byte OBJ_EVENT_CLICKED = 1;
            public const byte OBJ_EVENT_PRERENDER = 2;
            public const byte OBJ_EVENT_POSTRENDER = 3;
            public const byte OBJ_EVENT_PRESSED = 4;
            public const byte OBJ_EVENT_RELEASED = 5;

            /* Object states */
            public const byte OBJ_STATE_FREE = (1 << 0);
            public const byte OBJ_STATE_VALID = (1 << 1);
            public const byte OBJ_STATE_BUSY = (1 << 2);
            public const byte OBJ_STATE_VISIBLE = (1 << 3);
            public const byte OBJ_STATE_ENABLE = (1 << 4);
            public const byte OBJ_STATE_UPDATE = (1 << 5);
            public const byte OBJ_STATE_REDRAW = (1 << 6);
            public const byte OBJ_STATE_TOUCH_ENABLE = (1 << 7);
            public const byte OBJ_STATE_INIT = (OBJ_STATE_FREE | OBJ_STATE_VALID);

            /* Object touch states */
            public const byte OBJ_TOUCH_STATE_CHANGED = (1 << 0);
            public const byte OBJ_TOUCH_STATE_PRESSED_ON_OBJECT = (1 << 1);
            public const byte OBJ_TOUCH_STATE_PRESSED_OUTSIDE_OBJECT = (1 << 2);
            public const byte OBJ_TOUCH_STATE_RELEASED_ON_OBJECT = (1 << 3);
            public const byte OBJ_TOUCH_STATE_RELEASED_OUTSIDE_OBJECT = (1 << 4);
            public const byte OBJ_TOUCH_STATE_IS_PRESSED_ON_OBJECT = (1 << 5);
            public const byte OBJ_TOUCH_STATE_IS_PRESSED = (1 << 6);
            public const byte OBJ_TOUCH_STATE_CLICK_ON_OBJECT = (1 << 7);
            public const byte OBJ_TOUCH_STATE_INIT = 0;

            /* -------------------------------------------------------------------------------- */
            /* -- OBJECTS                                                                    -- */
            /* -------------------------------------------------------------------------------- */

            internal byte state { get; set; }
            internal byte touch_state { get; set; }
            internal ObjectUpdateDelegate update { get; set; }
            internal uGuiArea a_abs { get; set; }
            internal uGuiArea a_rel { get; set; }
            internal byte type { get; set; }
            internal byte id { get; set; }
            internal byte evt { get; set; }
            internal object data { get; set; }
            internal uGuiWindow parent { get; set; }

            public uGuiObject()
            {
                this.state = OBJ_STATE_INIT;
                this.data = null;
                this.evt = 0;
                this.id = 0;
                this.touch_state = 0;
                this.type = 0;
                this.update = null;
                this.parent = null;
            }
        }
    }
}
