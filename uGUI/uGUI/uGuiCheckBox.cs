using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiCheckBox
        {
            /* Default checkbox IDs */
            public const byte CHB_ID_0 = uGuiObject.OBJ_ID_0;
            public const byte CHB_ID_1 = uGuiObject.OBJ_ID_1;
            public const byte CHB_ID_2 = uGuiObject.OBJ_ID_2;
            public const byte CHB_ID_3 = uGuiObject.OBJ_ID_3;
            public const byte CHB_ID_4 = uGuiObject.OBJ_ID_4;
            public const byte CHB_ID_5 = uGuiObject.OBJ_ID_5;
            public const byte CHB_ID_6 = uGuiObject.OBJ_ID_6;
            public const byte CHB_ID_7 = uGuiObject.OBJ_ID_7;
            public const byte CHB_ID_8 = uGuiObject.OBJ_ID_8;
            public const byte CHB_ID_9 = uGuiObject.OBJ_ID_9;
            public const byte CHB_ID_10 = uGuiObject.OBJ_ID_10;
            public const byte CHB_ID_11 = uGuiObject.OBJ_ID_11;
            public const byte CHB_ID_12 = uGuiObject.OBJ_ID_12;
            public const byte CHB_ID_13 = uGuiObject.OBJ_ID_13;
            public const byte CHB_ID_14 = uGuiObject.OBJ_ID_14;
            public const byte CHB_ID_15 = uGuiObject.OBJ_ID_15;
            public const byte CHB_ID_16 = uGuiObject.OBJ_ID_16;
            public const byte CHB_ID_17 = uGuiObject.OBJ_ID_17;
            public const byte CHB_ID_18 = uGuiObject.OBJ_ID_18;
            public const byte CHB_ID_19 = uGuiObject.OBJ_ID_19;
            public const byte CHB_ID_20 = uGuiObject.OBJ_ID_20;
            public const byte CHB_ID_21 = uGuiObject.OBJ_ID_21;
            public const byte CHB_ID_22 = uGuiObject.OBJ_ID_22;
            public const byte CHB_ID_23 = uGuiObject.OBJ_ID_23;
            public const byte CHB_ID_24 = uGuiObject.OBJ_ID_24;
            public const byte CHB_ID_25 = uGuiObject.OBJ_ID_25;
            public const byte CHB_ID_26 = uGuiObject.OBJ_ID_26;
            public const byte CHB_ID_27 = uGuiObject.OBJ_ID_27;
            public const byte CHB_ID_28 = uGuiObject.OBJ_ID_28;
            public const byte CHB_ID_29 = uGuiObject.OBJ_ID_29;
            public const byte CHB_ID_30 = uGuiObject.OBJ_ID_30;
            public const byte CHB_ID_31 = uGuiObject.OBJ_ID_31;
            public const byte CHB_ID_32 = uGuiObject.OBJ_ID_32;
            public const byte CHB_ID_33 = uGuiObject.OBJ_ID_33;
            public const byte CHB_ID_34 = uGuiObject.OBJ_ID_34;
            public const byte CHB_ID_35 = uGuiObject.OBJ_ID_35;
            public const byte CHB_ID_36 = uGuiObject.OBJ_ID_36;
            public const byte CHB_ID_37 = uGuiObject.OBJ_ID_37;
            public const byte CHB_ID_38 = uGuiObject.OBJ_ID_38;
            public const byte CHB_ID_39 = uGuiObject.OBJ_ID_39;
            public const byte CHB_ID_40 = uGuiObject.OBJ_ID_40;
            public const byte CHB_ID_41 = uGuiObject.OBJ_ID_41;
            public const byte CHB_ID_42 = uGuiObject.OBJ_ID_42;
            public const byte CHB_ID_43 = uGuiObject.OBJ_ID_43;
            public const byte CHB_ID_44 = uGuiObject.OBJ_ID_44;
            public const byte CHB_ID_45 = uGuiObject.OBJ_ID_45;
            public const byte CHB_ID_46 = uGuiObject.OBJ_ID_46;
            public const byte CHB_ID_47 = uGuiObject.OBJ_ID_47;
            public const byte CHB_ID_48 = uGuiObject.OBJ_ID_48;
            public const byte CHB_ID_49 = uGuiObject.OBJ_ID_49;
            public const byte CHB_ID_50 = uGuiObject.OBJ_ID_50;

            /* Checkbox states */
            public const byte CHB_STATE_RELEASED = (0<<0);
            public const byte CHB_STATE_PRESSED = (1<<0);
            public const byte CHB_STATE_ALWAYS_REDRAW = (1<<1);

            /* Checkbox style */
            public const byte CHB_STYLE_2D = (0<<0);
            public const byte CHB_STYLE_3D = (1<<0);
            public const byte CHB_STYLE_TOGGLE_COLORS = (1<<1);
            public const byte CHB_STYLE_USE_ALTERNATE_COLORS = (1<<2);
            public const byte CHB_STYLE_NO_BORDERS = (1<<3);
            public const byte CHB_STYLE_NO_FILL = (1 << 4);
            
            /* Checkbox events */
            public const byte CHB_EVENT_CLICKED = uGuiObject.OBJ_EVENT_CLICKED;

            /* -------------------------------------------------------------------------------- */
            /* -- CHECKBOX OBJECT                                                            -- */
            /* -------------------------------------------------------------------------------- */

            internal byte state { get; set; }
            internal byte style { get; set; }
            internal uGuiColor fc { get; set; }
            internal uGuiColor bc { get; set; }
            internal uGuiColor afc { get; set; }
            internal uGuiColor abc { get; set; }
            internal uGuiFont font { get; set; }
            internal byte align { get; set; }
            internal sbyte h_space { get; set; }
            internal sbyte v_space { get; set; }
            internal string str { get; set; }
            internal bool check { get; set; }
            internal uGuiObject container { get; set; }

            public uGuiCheckBox( uGuiWindow wnd, byte id, Int16 xs, Int16 ys, Int16 xe, Int16 ye )
            {
                uGuiObject obj = new uGuiObject();

                if (wnd == null)
                {
                    throw new ArgumentNullException("wnd");
                }

                /* Initialize object-specific parameters */
                this.state = CHB_STATE_RELEASED;
                this.bc = wnd.bc;
                this.fc = wnd.fc;
                this.abc = wnd.bc;
                this.afc = wnd.fc;
                this.style = CHB_STYLE_2D;
                this.align = ALIGN_TOP_LEFT;
                if (gui != null) 
                {
                    this.font = gui.font;
                }
                else 
                {
                    this.font = null;
                }
                this.str = "-";
                this.check = false;
                this.container = obj;

                /* Initialize standard object parameters */
                obj.update = CheckboxUpdate;
                obj.parent = wnd;
                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.type = uGuiObject.OBJ_TYPE_CHECKBOX;
                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.a_rel = new uGuiArea(xs, ys, xe, ye);
                obj.a_abs = new uGuiArea();
                obj.id = id;
                obj.state |= uGuiObject.OBJ_STATE_VISIBLE | uGuiObject.OBJ_STATE_REDRAW | uGuiObject.OBJ_STATE_VALID | uGuiObject.OBJ_STATE_TOUCH_ENABLE;
                obj.data = this;

                /* Update function: Do your thing! */
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_FREE & 0xFF);
                wnd.AddObject(obj);
            }

            public static sbyte CheckboxDelete(uGuiWindow wnd, byte id)
            {
                if (wnd != null)
                {
                    return wnd.DeleteObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                }
                return UG_RESULT_FAIL;
            }

            public sbyte Delete()
            {
                return this.container.parent.DeleteObject(uGuiObject.OBJ_TYPE_CHECKBOX, this.container.id);
            }

            public static sbyte CheckboxShow(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;

                obj.state |= uGuiObject.OBJ_STATE_VISIBLE;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte Show()
            {
                if (this.container == null) return UG_RESULT_FAIL;
                this.container.state |= uGuiObject.OBJ_STATE_VISIBLE;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;
                return UG_RESULT_OK;
            }

            public static sbyte CheckboxHide( uGuiWindow wnd, byte id )
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);

                chb.state &= (byte)(~CHB_STATE_PRESSED & 0xFF);
                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                obj.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public sbyte Hide()
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.state &= (byte)(~CHB_STATE_PRESSED & 0xFF);
                this.container.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                this.container.evt = uGuiObject.OBJ_EVENT_NONE;
                this.container.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }
                       
            public static sbyte CheckboxSetChecked( uGuiWindow wnd, byte id, bool ch )
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);

                chb.check = ch;                
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte Checked(bool ch)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.check = ch;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetForeColor(uGuiWindow wnd, byte id, uGuiColor fc)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (fc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.fc = fc;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetForeColor(uGuiColor fc)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.fc = fc;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetBackColor(uGuiWindow wnd, byte id, uGuiColor bc)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (bc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.bc = bc;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetBackColor(uGuiColor bc)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.bc = bc;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetAlternateForeColor(uGuiWindow wnd, byte id, uGuiColor afc)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (afc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.afc = afc;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetAlternateForeColor(uGuiColor afc)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.afc = afc;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetAlternateBackColor(uGuiWindow wnd, byte id, uGuiColor abc)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (abc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.abc = abc;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetAlternateBackColor(uGuiColor abc)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.abc = abc;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }
            
            public static sbyte CheckboxSetText(uGuiWindow wnd, byte id, string str)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (str == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.str = str;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetText(string str)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.str = str;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetFont(uGuiWindow wnd, byte id, uGuiFont font)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (font == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.font = font;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetFont(uGuiFont font)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.font = font;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetStyle(uGuiWindow wnd, byte id, byte style)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                /* Select color scheme */
                chb.style &= (byte)(~(CHB_STYLE_USE_ALTERNATE_COLORS | CHB_STYLE_TOGGLE_COLORS | CHB_STYLE_NO_BORDERS | CHB_STYLE_NO_FILL) & 0xFF);
                chb.state |= CHB_STATE_ALWAYS_REDRAW;
                if ((style & CHB_STYLE_NO_BORDERS) != 0)
                {
                    chb.style |= CHB_STYLE_NO_BORDERS;
                }
                if ((style & CHB_STYLE_NO_FILL) != 0)
                {
                    chb.style |= CHB_STYLE_NO_FILL;
                }
                if ((style & CHB_STYLE_TOGGLE_COLORS) != 0)
                {
                    chb.style |= CHB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & CHB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    chb.style |= CHB_STYLE_USE_ALTERNATE_COLORS;
                }
                else
                {
                    chb.state &= (byte)(~CHB_STATE_ALWAYS_REDRAW & 0xFF);
                }

                /* 3D or 2D */
                if ((style & CHB_STYLE_3D) != 0)
                {
                    chb.style |= CHB_STYLE_3D;
                }
                else
                {
                    chb.style &= (byte)(~CHB_STYLE_3D & 0xFF);
                }
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetStyle(byte style)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                /* Select color scheme */
                this.style &= (byte)(~(CHB_STYLE_USE_ALTERNATE_COLORS | CHB_STYLE_TOGGLE_COLORS | CHB_STYLE_NO_BORDERS | CHB_STYLE_NO_FILL) & 0xFF);
                this.state |= CHB_STATE_ALWAYS_REDRAW;
                if ((style & CHB_STYLE_NO_BORDERS) != 0)
                {
                    this.style |= CHB_STYLE_NO_BORDERS;
                }
                if ((style & CHB_STYLE_NO_FILL) != 0)
                {
                    this.style |= CHB_STYLE_NO_FILL;
                }
                if ((style & CHB_STYLE_TOGGLE_COLORS) != 0)
                {
                    this.style |= CHB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & CHB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    this.style |= CHB_STYLE_USE_ALTERNATE_COLORS;
                }
                else
                {
                    this.state &= (byte)(~CHB_STATE_ALWAYS_REDRAW & 0xFF);
                }

                /* 3D or 2D */
                if ((style & CHB_STYLE_3D) != 0)
                {
                    this.style |= CHB_STYLE_3D;
                }
                else
                {
                    this.style &= (byte)(~CHB_STYLE_3D & 0xFF);
                }
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetHSpace(uGuiWindow wnd, byte id, sbyte hs)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.h_space = hs;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }
            
            public sbyte SetHSpace(sbyte hs)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.h_space = hs;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetVSpace(uGuiWindow wnd, byte id, sbyte vs)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.v_space = vs;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetVSpace(sbyte vs)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.v_space = vs;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte CheckboxSetAlignment(uGuiWindow wnd, byte id, byte align)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                chb = (uGuiCheckBox)(obj.data);
                chb.align = align;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetAlignment(byte align)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.align = align;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }
            
            public static bool CheckboxGetChecked( uGuiWindow wnd, byte id )
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return false;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return false;
                if (obj.data == null) return false;

                chb = (uGuiCheckBox)(obj.data);               

                return chb.check;
            }

            public bool GetChecked()
            {
                return this.check;
            }
            
            public static uGuiColor CheckboxGetForeColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.fc;
            }

            public uGuiColor GetForeColor()
            {
                return this.fc;
            }

            public static uGuiColor CheckboxGetBackColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.bc;
            }

            public uGuiColor GetBackColor()
            {
                return this.bc;
            }

            public static uGuiColor CheckboxGetAlternateForeColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.afc;
            }

            public uGuiColor GetAlternateForeColor()
            {
                return this.afc;
            }

            public static uGuiColor CheckboxGetAlternateBackColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.abc;
            }

            public uGuiColor GetAlternateBackColor()
            {
                return this.abc;
            }

            public static string CheckboxGetText(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.str;
            }

            public string GetText()
            {
                return this.str;
            }

            public static uGuiFont CheckboxGetFont(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                chb = (uGuiCheckBox)(obj.data);
                return chb.font;
            }

            public uGuiFont GetFont()
            {
                return this.font;
            }

            public static byte CheckboxGetStyle(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                chb = (uGuiCheckBox)(obj.data);
                return chb.style;
            }

            public byte GetStyle()
            {
                return this.style;
            }

            public static sbyte CheckboxGetHSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                chb = (uGuiCheckBox)(obj.data);
                return chb.h_space;
            }

            public sbyte GetHSpace()
            {
                return this.h_space;
            }

            public static sbyte CheckboxGetVSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                chb = (uGuiCheckBox)(obj.data);
                return chb.v_space;
            }

            public sbyte GetVSpace()
            {
                return this.v_space;
            }

            public static byte CheckboxGetAlignment(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiCheckBox chb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_CHECKBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                chb = (uGuiCheckBox)(obj.data);
                return chb.align;
            }

            public byte GetAlignment()
            {
                return this.align;
            }
            
            private void CheckboxUpdate()
            {
                uGuiArea a = new uGuiArea();
                uGuiText txt = new uGuiText();
                byte d;
                byte d2;

                /* -------------------------------------------------- */
                /* Object touch section                               */
                /* -------------------------------------------------- */
                if ( (this.container.touch_state & uGuiObject.OBJ_TOUCH_STATE_CHANGED) != 0 )
                {
                    /* Handle 'click' event */
                    if (( this.container.touch_state & uGuiObject.OBJ_TOUCH_STATE_CLICK_ON_OBJECT ) != 0)
                    {
                        this.container.evt = CHB_EVENT_CLICKED;
                        this.container.state |= uGuiObject.OBJ_STATE_UPDATE;
                    }
                    /* Is the Checkbox pressed down? */
                    if (( this.container.touch_state & uGuiObject.OBJ_TOUCH_STATE_PRESSED_ON_OBJECT ) != 0)
                    {
                        this.state |= CHB_STATE_PRESSED;
                        this.container.state |= uGuiObject.OBJ_STATE_UPDATE;
                        this.container.evt = uGuiObject.OBJ_EVENT_PRESSED;
                    }
                    /* Can we release the Checkbox? */
                    else if (( this.state & CHB_STATE_PRESSED ) != 0)
                    {
                        this.state &= (byte)(~CHB_STATE_PRESSED & 0xFF);
                        this.container.state |= uGuiObject.OBJ_STATE_UPDATE;
                        this.container.evt = uGuiObject.OBJ_EVENT_RELEASED;
          
                        this.check = !this.check; 
                    }
                    this.container.touch_state &= (byte)(~uGuiObject.OBJ_TOUCH_STATE_CHANGED & 0xFF);
                }

                /* -------------------------------------------------- */
                /* Object update section                              */
                /* -------------------------------------------------- */
                if (( this.container.state & uGuiObject.OBJ_STATE_UPDATE ) != 0)
                {
                    this.container.parent.GetArea(a);
                    this.container.a_abs.xs = (Int16)(this.container.a_rel.xs + a.xs);
                    this.container.a_abs.ys = (Int16)(this.container.a_rel.ys + a.ys);
                    this.container.a_abs.xe = (Int16)(this.container.a_rel.xe + a.xs);
                    this.container.a_abs.ye = (Int16)(this.container.a_rel.ye + a.ys);
                    if ( this.container.a_abs.ye > this.container.parent.ye ) return;
                    if ( this.container.a_abs.xe > this.container.parent.xe ) return;
       
                    if (( this.container.state & uGuiObject.OBJ_STATE_VISIBLE ) != 0)
                    {
                        /* 3D or 2D style? */
                        d  = ( this.style & CHB_STYLE_3D ) != 0 ? (byte)3 : (byte)1;
                        d2 = (this.font.char_width < this.font.char_height) ? (byte)this.font.char_height : (byte)this.font.char_width;
          
                        /* Full redraw necessary? */
                        if (((this.container.state & uGuiObject.OBJ_STATE_REDRAW) != 0) || ((this.state & CHB_STATE_ALWAYS_REDRAW) != 0))
                        {
                            this.container.parent.SendObjectPrerenderEvent(this.container);
                            txt.bc = this.bc;
                            txt.fc = this.fc;

                            if(( this.state & CHB_STATE_PRESSED ) != 0)
                            {
                                /* "toggle" style? */
                                if(( this.style & CHB_STYLE_TOGGLE_COLORS ) != 0)
                                {
                                    /* Swap colors */
                                    txt.bc = this.fc;
                                    txt.fc = this.bc;
                                }
                                /* Use alternate colors? */
                                else if (( this.style & CHB_STYLE_USE_ALTERNATE_COLORS ) != 0)
                                {
                                    txt.bc = this.abc;
                                    txt.fc = this.afc;
                                }
                            }
                            if ((this.style & CHB_STYLE_NO_FILL) == 0)
                            {
                                FillFrame((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xe - d), (Int16)(this.container.a_abs.ye - d), txt.bc);
                            }

                            /* Draw Checkbox text */
                            //             txt.a.xs = this.container.a_abs.xs+d;
                            //             txt.a.ys = this.container.a_abs.ys+d;
                            //             txt.a.xe = this.container.a_abs.xe-d;
                            //             txt.a.ye = this.container.a_abs.ye-d;
                            txt.a = new uGuiArea();
                            txt.a.xs = (Int16)(this.container.a_abs.xs + d2 + 3*d);
                            txt.a.ys = (Int16)(this.container.a_abs.ys + d);
                            txt.a.xe = this.container.a_abs.xe;
                            txt.a.ye = this.container.a_abs.ye;
                            txt.align = this.align;
                            txt.font = this.font;
                            txt.h_space = 2;
                            txt.v_space = 2;
                            txt.str = this.str;
                            PutText( txt );
                            this.container.state &= (byte)(~uGuiObject.OBJ_STATE_REDRAW & 0xFF);
                            this.container.parent.SendObjectPostrenderEvent(this.container);
                        }
         
                        /* Draw Checkbox X */
                        if (this.check)
                        {
                            DrawLine((Int16)(this.container.a_abs.xs + d + 1), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d2 + d - 2), this.fc);
                            DrawLine((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.fc);
                            DrawLine((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d + 1), (Int16)(this.container.a_abs.xs + d2 + d - 2), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.fc);

                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d + 1), (Int16)(this.container.a_abs.xs + d + 1), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.fc);
                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.fc);
                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 2), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d2 + d - 2), this.fc);
                        }
                        else
                        {
                            DrawLine((Int16)(this.container.a_abs.xs + d + 1), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d2 + d - 2), this.bc);
                            DrawLine((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.bc);
                            DrawLine((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d + 1), (Int16)(this.container.a_abs.xs + d2 + d - 2), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.bc);

                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d + 1), (Int16)(this.container.a_abs.xs + d + 1), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.bc);
                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 1), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d2 + d - 1), this.bc);
                            DrawLine((Int16)(this.container.a_abs.xs + d2 + d - 2), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d2 + d - 2), this.bc);
                        }    
                        /* Draw Checkbox frame */
                        if ((this.style & CHB_STYLE_NO_BORDERS) == 0)
                        {
                            if (( this.style & CHB_STYLE_3D ) != 0)
                            {  /* 3D */
                                DrawObjectFrame(this.container.a_abs.xs, this.container.a_abs.ys, (Int16)(this.container.a_abs.xs + d2 + 2 * d - 1), (Int16)(this.container.a_abs.ys + d2 + 2 * d - 1), (this.state & CHB_STATE_PRESSED) != 0 ? pal_checkbox_pressed : pal_checkbox_released);
                            }
                            else
                            {  /* 2D */
                                DrawFrame(this.container.a_abs.xs, this.container.a_abs.ys, (Int16)(this.container.a_abs.xs + d2 + 2 * d - 1), (Int16)(this.container.a_abs.ys + d2 + 2 * d - 1), (this.state & CHB_STATE_PRESSED) != 0 ? this.abc : this.afc);
                            }
                        }
                    }
                    else
                    {
                        if ((this.style & CHB_STYLE_NO_FILL) == 0)
                        {
                            FillFrame(this.container.a_abs.xs, this.container.a_abs.ys, this.container.a_abs.xe, this.container.a_abs.ye, this.container.parent.bc);
                        }
                    }
                    this.container.state &= (byte)(~uGuiObject.OBJ_STATE_UPDATE & 0xFF);
                }
            }

        }
    }
}
