using System;
using System.Collections;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiListBox
        {
            /* Default Listbox IDs */
            public const byte LSB_ID_0 = uGuiObject.OBJ_ID_0;
            public const byte LSB_ID_1 = uGuiObject.OBJ_ID_1;
            public const byte LSB_ID_2 = uGuiObject.OBJ_ID_2;
            public const byte LSB_ID_3 = uGuiObject.OBJ_ID_3;
            public const byte LSB_ID_4 = uGuiObject.OBJ_ID_4;
            public const byte LSB_ID_5 = uGuiObject.OBJ_ID_5;
            public const byte LSB_ID_6 = uGuiObject.OBJ_ID_6;
            public const byte LSB_ID_7 = uGuiObject.OBJ_ID_7;
            public const byte LSB_ID_8 = uGuiObject.OBJ_ID_8;
            public const byte LSB_ID_9 = uGuiObject.OBJ_ID_9;
            public const byte LSB_ID_10 = uGuiObject.OBJ_ID_10;
            public const byte LSB_ID_11 = uGuiObject.OBJ_ID_11;
            public const byte LSB_ID_12 = uGuiObject.OBJ_ID_12;
            public const byte LSB_ID_13 = uGuiObject.OBJ_ID_13;
            public const byte LSB_ID_14 = uGuiObject.OBJ_ID_14;
            public const byte LSB_ID_15 = uGuiObject.OBJ_ID_15;
            public const byte LSB_ID_16 = uGuiObject.OBJ_ID_16;
            public const byte LSB_ID_17 = uGuiObject.OBJ_ID_17;
            public const byte LSB_ID_18 = uGuiObject.OBJ_ID_18;
            public const byte LSB_ID_19 = uGuiObject.OBJ_ID_19;
            public const byte LSB_ID_20 = uGuiObject.OBJ_ID_20;
            public const byte LSB_ID_21 = uGuiObject.OBJ_ID_21;
            public const byte LSB_ID_22 = uGuiObject.OBJ_ID_22;
            public const byte LSB_ID_23 = uGuiObject.OBJ_ID_23;
            public const byte LSB_ID_24 = uGuiObject.OBJ_ID_24;
            public const byte LSB_ID_25 = uGuiObject.OBJ_ID_25;
            public const byte LSB_ID_26 = uGuiObject.OBJ_ID_26;
            public const byte LSB_ID_27 = uGuiObject.OBJ_ID_27;
            public const byte LSB_ID_28 = uGuiObject.OBJ_ID_28;
            public const byte LSB_ID_29 = uGuiObject.OBJ_ID_29;
            public const byte LSB_ID_30 = uGuiObject.OBJ_ID_30;
            public const byte LSB_ID_31 = uGuiObject.OBJ_ID_31;
            public const byte LSB_ID_32 = uGuiObject.OBJ_ID_32;
            public const byte LSB_ID_33 = uGuiObject.OBJ_ID_33;
            public const byte LSB_ID_34 = uGuiObject.OBJ_ID_34;
            public const byte LSB_ID_35 = uGuiObject.OBJ_ID_35;
            public const byte LSB_ID_36 = uGuiObject.OBJ_ID_36;
            public const byte LSB_ID_37 = uGuiObject.OBJ_ID_37;
            public const byte LSB_ID_38 = uGuiObject.OBJ_ID_38;
            public const byte LSB_ID_39 = uGuiObject.OBJ_ID_39;
            public const byte LSB_ID_40 = uGuiObject.OBJ_ID_40;
            public const byte LSB_ID_41 = uGuiObject.OBJ_ID_41;
            public const byte LSB_ID_42 = uGuiObject.OBJ_ID_42;
            public const byte LSB_ID_43 = uGuiObject.OBJ_ID_43;
            public const byte LSB_ID_44 = uGuiObject.OBJ_ID_44;
            public const byte LSB_ID_45 = uGuiObject.OBJ_ID_45;
            public const byte LSB_ID_46 = uGuiObject.OBJ_ID_46;
            public const byte LSB_ID_47 = uGuiObject.OBJ_ID_47;
            public const byte LSB_ID_48 = uGuiObject.OBJ_ID_48;
            public const byte LSB_ID_49 = uGuiObject.OBJ_ID_49;
            public const byte LSB_ID_50 = uGuiObject.OBJ_ID_50;



            /* Listbox style */
            public const byte LSB_STYLE_2D = (0 << 0);
            public const byte LSB_STYLE_3D = (1 << 0);
            public const byte LSB_STYLE_TOGGLE_COLORS = (1 << 1);
            public const byte LSB_STYLE_USE_ALTERNATE_COLORS = (1 << 2);
            public const byte LSB_STYLE_NO_BORDERS = (1 << 3);
            public const byte LSB_STYLE_NO_FILL = (1 << 4);
            public const byte LSB_STYLE_USE_SCROLL = (1 << 5);


            /* -------------------------------------------------------------------------------- */
            /* -- LISTBOX OBJECT                                                            -- */
            /* -------------------------------------------------------------------------------- */

            internal byte style { get; set; }
            internal uGuiColor fc { get; set; }
            internal uGuiColor bc { get; set; }
            internal uGuiColor afc { get; set; }
            internal uGuiColor abc { get; set; }
            internal uGuiFont font { get; set; }
            internal byte align { get; set; }
            internal sbyte h_space { get; set; }
            internal sbyte v_space { get; set; }
            internal ArrayList str { get; set; }
            internal int index { get; set; }
            internal bool check { get; set; }
            internal uGuiObject container { get; set; }

            public uGuiListBox(uGuiWindow wnd, byte id, Int16 xs, Int16 ys, Int16 xe, Int16 ye)
            {
                uGuiObject obj = new uGuiObject();

                if (wnd == null)
                {
                    throw new ArgumentNullException("wnd");
                }

                /* Initialize object-specific parameters */
                this.bc = wnd.bc;
                this.fc = wnd.fc;
                this.abc = wnd.bc;
                this.afc = wnd.fc;
                this.style = LSB_STYLE_2D | LSB_STYLE_USE_SCROLL;
                this.align = ALIGN_TOP_LEFT;
                if (gui != null)
                {
                    this.font = gui.font;
                }
                else
                {
                    this.font = null;
                }
                this.str = new ArrayList();
                this.index = -1;
                this.check = false;
                this.container = obj;

                /* Initialize standard object parameters */
                obj.update = ListboxUpdate;
                obj.parent = wnd;
                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.type = uGuiObject.OBJ_TYPE_LISTBOX;
                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.a_rel = new uGuiArea(xs, ys, xe, ye);
                obj.a_abs = new uGuiArea();
                obj.id = id;
                obj.state |= uGuiObject.OBJ_STATE_VISIBLE | uGuiObject.OBJ_STATE_REDRAW | uGuiObject.OBJ_STATE_VALID; // | uGuiObject.OBJ_STATE_TOUCH_ENABLE;
                obj.data = this;

                /* Update function: Do your thing! */
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_FREE & 0xFF);
                wnd.AddObject(obj);
            }
            
            public static sbyte ListboxDelete(uGuiWindow wnd, byte id)
            {
                if (wnd != null)
                {
                    return wnd.DeleteObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                }
                return UG_RESULT_FAIL;
            }

            public sbyte Delete()
            {
                return this.container.parent.DeleteObject(uGuiObject.OBJ_TYPE_LISTBOX, this.container.id);
            }

            public static sbyte ListboxShow(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
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

            public static sbyte ListboxHide(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                obj.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public sbyte Hide()
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.container.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                this.container.evt = uGuiObject.OBJ_EVENT_NONE;
                this.container.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public static sbyte ListboxSetIndex(uGuiWindow wnd, byte id, int index)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (index < -1) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    lsb.str = new ArrayList();
                }

                if (index < lsb.str.Count)
                {
                    lsb.index = index;
                    obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                    return UG_RESULT_OK;
                }
                else
                {
                    return UG_RESULT_FAIL;
                }
            }

            public sbyte SetIndex(int index)
            {
                if (this.container == null) return UG_RESULT_FAIL;
                if (index < -1) return UG_RESULT_FAIL;

                if (this.str == null)
                {
                    this.str = new ArrayList();
                }

                if (index < this.str.Count)
                {
                    this.index = index;
                    this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;
                    return UG_RESULT_OK;
                }
                else
                {
                    return UG_RESULT_FAIL;
                }
            }
            
            public static sbyte ListboxAddItem(uGuiWindow wnd, byte id, string item)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (item == null) return UG_RESULT_FAIL;
                if (item.Length == 0) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    lsb.str = new ArrayList();
                }
                lsb.str.Add(item);
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte AddItem(string item)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                if (this.str == null)
                {
                    this.str = new ArrayList();
                }
                this.str.Add(item);
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;
                return UG_RESULT_OK;
            }

            public static sbyte ListboxRemoveItem(uGuiWindow wnd, byte id, int index)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (index < 0) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    lsb.str = new ArrayList();
                }
                if (index >= lsb.str.Count)
                {
                    return UG_RESULT_FAIL;
                }
                lsb.str.RemoveAt(index);
                if (lsb.index >= lsb.str.Count)
                {
                    lsb.index = lsb.str.Count - 1;
                }
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte RemoveItem(int index)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                if (this.str == null)
                {
                    this.str = new ArrayList();
                }
                if (index >= this.str.Count)
                {
                    return UG_RESULT_FAIL;
                }
                this.str.RemoveAt(index);
                if (this.index >= this.str.Count)
                {
                    this.index = this.str.Count - 1;
                }
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;
                return UG_RESULT_OK;
            }

            public static sbyte ListboxClearItems(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    lsb.str = new ArrayList();
                }
                lsb.str.Clear();
                lsb.index = -1;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte ClearItems()
            {
                if (this.container == null) return UG_RESULT_FAIL;

                if (this.str == null)
                {
                    this.str = new ArrayList();
                }
                this.str.Clear();
                this.index = -1;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;
                return UG_RESULT_OK;
            }

            public static sbyte ListboxSetForeColor(uGuiWindow wnd, byte id, uGuiColor fc)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (fc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.fc = fc;
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

            public static sbyte ListboxSetBackColor(uGuiWindow wnd, byte id, uGuiColor bc)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (bc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.bc = bc;
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

            public static sbyte ListboxSetAlternateForeColor(uGuiWindow wnd, byte id, uGuiColor afc)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (afc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.afc = afc;
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

            public static sbyte ListboxSetAlternateBackColor(uGuiWindow wnd, byte id, uGuiColor abc)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (abc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.abc = abc;
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

            public static sbyte ListboxSetFont(uGuiWindow wnd, byte id, uGuiFont font)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (font == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.font = font;
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

            public static sbyte ListboxSetStyle(uGuiWindow wnd, byte id, byte style)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                /* Select color scheme */
                lsb.style &= (byte)(~(LSB_STYLE_USE_ALTERNATE_COLORS | LSB_STYLE_TOGGLE_COLORS | LSB_STYLE_NO_BORDERS | LSB_STYLE_NO_FILL) & 0xFF);
                if ((style & LSB_STYLE_NO_BORDERS) != 0)
                {
                    lsb.style |= LSB_STYLE_NO_BORDERS;
                }
                if ((style & LSB_STYLE_NO_FILL) != 0)
                {
                    lsb.style |= LSB_STYLE_NO_FILL;
                }
                if ((style & LSB_STYLE_TOGGLE_COLORS) != 0)
                {
                    lsb.style |= LSB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & LSB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    lsb.style |= LSB_STYLE_USE_ALTERNATE_COLORS;
                }

                /* 3D or 2D */
                if ((style & LSB_STYLE_3D) != 0)
                {
                    lsb.style |= LSB_STYLE_3D;
                }
                else
                {
                    lsb.style &= (byte)(~LSB_STYLE_3D & 0xFF);
                }
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetStyle(byte style)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                /* Select color scheme */
                this.style &= (byte)(~(LSB_STYLE_USE_ALTERNATE_COLORS | LSB_STYLE_TOGGLE_COLORS | LSB_STYLE_NO_BORDERS | LSB_STYLE_NO_FILL) & 0xFF);
                if ((style & LSB_STYLE_NO_BORDERS) != 0)
                {
                    this.style |= LSB_STYLE_NO_BORDERS;
                }
                if ((style & LSB_STYLE_NO_FILL) != 0)
                {
                    this.style |= LSB_STYLE_NO_FILL;
                }
                if ((style & LSB_STYLE_TOGGLE_COLORS) != 0)
                {
                    this.style |= LSB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & LSB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    this.style |= LSB_STYLE_USE_ALTERNATE_COLORS;
                }

                /* 3D or 2D */
                if ((style & LSB_STYLE_3D) != 0)
                {
                    this.style |= LSB_STYLE_3D;
                }
                else
                {
                    this.style &= (byte)(~LSB_STYLE_3D & 0xFF);
                }
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte ListboxSetHSpace(uGuiWindow wnd, byte id, sbyte hs)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.h_space = hs;
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

            public static sbyte ListboxSetVSpace(uGuiWindow wnd, byte id, sbyte vs)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.v_space = vs;
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

            public static sbyte ListboxSetAlignment(uGuiWindow wnd, byte id, byte align)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                lsb.align = align;
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

            public static int ListboxGetIndex(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);
                return lsb.index;
            }

            public int GetIndex()
            {
                if (this.container == null) return UG_RESULT_FAIL;
                return this.index;
            }

            public static string ListboxGetItem(uGuiWindow wnd, byte id, int index)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                if (index < 0) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    return null;
                }
                if (index >= lsb.str.Count)
                {
                    return null;
                }
                return (string)lsb.str[index];
            }

            public string GetItem(int index)
            {
                if (this.container == null) return null;
                if (index < 0) return null;
                if (this.str == null)
                {
                    return null;
                }
                if (index >= this.str.Count)
                {
                    return null;
                }
                return (string)this.str[index];
            }

            public static int ListboxGetItemsCount(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                lsb = (uGuiListBox)(obj.data);

                if (lsb.str == null)
                {
                    return UG_RESULT_FAIL;
                }
                return lsb.str.Count;
            }

            public int GetItemsCount()
            {
                if (this.container == null) return UG_RESULT_FAIL;
                if (this.str == null)
                {
                    return UG_RESULT_FAIL;
                }
                return this.str.Count;
            }

            public static uGuiColor ListboxGetForeColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);
                return lsb.fc;
            }

            public uGuiColor GetForeColor()
            {
                return this.fc;
            }

            public static uGuiColor ListboxGetBackColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);
                return lsb.bc;
            }

            public uGuiColor GetBackColor()
            {
                return this.bc;
            }

            public static uGuiColor ListboxGetAlternateForeColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);
                return lsb.afc;
            }

            public uGuiColor GetAlternateForeColor()
            {
                return this.afc;
            }

            public static uGuiColor ListboxGetAlternateBackColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);
                return lsb.abc;
            }

            public uGuiColor GetAlternateBackColor()
            {
                return this.abc;
            }

            public static uGuiFont ListboxGetFont(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                lsb = (uGuiListBox)(obj.data);
                return lsb.font;
            }

            public uGuiFont GetFont()
            {
                return this.font;
            }

            public static byte ListboxGetStyle(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                lsb = (uGuiListBox)(obj.data);
                return lsb.style;
            }

            public byte GetStyle()
            {
                return this.style;
            }

            public static sbyte ListboxGetHSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                lsb = (uGuiListBox)(obj.data);
                return lsb.h_space;
            }

            public sbyte GetHSpace()
            {
                return this.h_space;
            }

            public static sbyte ListboxGetVSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                lsb = (uGuiListBox)(obj.data);
                return lsb.v_space;
            }

            public sbyte GetVSpace()
            {
                return this.v_space;
            }

            public static byte ListboxGetAlignment(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiListBox lsb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_LISTBOX, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                lsb = (uGuiListBox)(obj.data);
                return lsb.align;
            }

            public byte GetAlignment()
            {
                return this.align;
            }
            
            private void ListboxUpdate()
            {
                if (this.container == null) return;
                if (this.container.parent == null) return;

                uGuiArea a = new uGuiArea();
                uGuiText txt = new uGuiText();
                byte d;

                /* -------------------------------------------------- */
                /* Object touch section                               */
                /* -------------------------------------------------- */

                /* Textbox doesn't support touch */

                /* -------------------------------------------------- */
                /* Object update section                              */
                /* -------------------------------------------------- */

                if ((this.container.state & uGuiObject.OBJ_STATE_UPDATE) != 0)
                {
                    if ((this.container.state & uGuiObject.OBJ_STATE_VISIBLE) != 0)
                    {
                        /* Full redraw necessary? */
                        if ((this.container.state & uGuiObject.OBJ_STATE_REDRAW) != 0)
                        {
                            this.container.parent.GetArea(a);
                            this.container.a_abs.xs = (Int16)(this.container.a_rel.xs + a.xs);
                            this.container.a_abs.ys = (Int16)(this.container.a_rel.ys + a.ys);
                            this.container.a_abs.xe = (Int16)(this.container.a_rel.xe + a.xs);
                            this.container.a_abs.ye = (Int16)(this.container.a_rel.ye + a.ys);
                            if (this.container.a_abs.ye >= this.container.parent.ye) return;
                            if (this.container.a_abs.xe >= this.container.parent.xe) return;
                            this.container.parent.SendObjectPrerenderEvent(this.container);

                            /* 3D or 2D style? */
                            d = (this.style & LSB_STYLE_3D) != 0 ? (byte)3 : (byte)1;

                            


                            if ((this.style & LSB_STYLE_NO_FILL) == 0)
                            {
                                FillFrame((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xe - d), (Int16)(this.container.a_abs.ye - d), this.bc);
                            }
                            
                            /* Draw Listbox text */
                            if ((this.str != null) && (this.str.Count > 0))
                            {
                                Int16 yp = (Int16)(this.container.a_abs.ys + d);
                                int i;
                                string s;
                                if (this.index < 0)
                                {
                                    i = 0;
                                }
                                else
                                {
                                    i = this.index;
                                }
                                while ((i < this.str.Count) && ((yp + this.font.char_height + this.v_space) < (this.container.a_abs.ye - d)))
                                {
                                    s = this.str[i] as string;
                                    if(i == this.index)
                                    {
                                        if((this.style & LSB_STYLE_USE_ALTERNATE_COLORS) != 0)
                                        {
                                            txt.bc = this.abc;
                                            txt.fc = this.afc;
                                        }
                                        else
                                        {
                                            txt.bc = this.fc;
                                            txt.fc = this.bc;
                                        }
                                    }
                                    else
                                    {
                                        txt.bc = this.bc;
                                        txt.fc = this.fc;
                                    }
                                    txt.a = new uGuiArea();
                                    txt.a.xs = (Int16)(this.container.a_abs.xs + d);
                                    txt.a.ys = yp;
                                    txt.a.xe = (Int16)(this.container.a_abs.xe - d);
                                    txt.a.ye = (Int16)(yp + this.font.char_height + this.v_space);
                                    txt.align = this.align;
                                    txt.font = this.font;
                                    txt.h_space = this.h_space;
                                    txt.v_space = this.v_space;
                                    txt.str = s;
                                    yp = (Int16)(txt.a.ye);
                                    i++;
                                    FillFrame(txt.a.xs, txt.a.ys, txt.a.xe, txt.a.ye, txt.bc);
                                    PutText(txt);
                                }
                            }
                             
                            if ((this.style & LSB_STYLE_USE_SCROLL) != 0)
                            {
                                uGuiArea abs = new uGuiArea((Int16)(this.container.a_abs.xe - 4 - d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xe - d), (Int16)(this.container.a_abs.ye - d));
                                Int16 sh = (Int16)(abs.ye - abs.ys - 2);
                                if((sh > 0) && (abs.xe > abs.xs))
                                {   
                                    FillFrame(abs.xs, abs.ys, abs.xe, abs.ye, this.bc);
                                    uGuiArea scroll = new uGuiArea(1,0,3,1);
                                    Int16 step = (Int16)(sh / this.str.Count);
                                    if (step > 0)
                                    {
                                        scroll.ye = step;
                                    }
                                    if (this.index > 0)
                                    {
                                        scroll.ys = (Int16)((sh * this.index) / this.str.Count);
                                        scroll.ye += scroll.ys;
                                    }
                                    scroll.xs += abs.xs;
                                    scroll.ys += (Int16)(abs.ys + 1);
                                    scroll.xe += abs.xs;
                                    scroll.ye += (Int16)(abs.ys + 1);
                                    FillFrame(scroll.xs, scroll.ys, scroll.xe, scroll.ye, this.fc);
                                }
                            }

                            this.container.state &= (byte)(~uGuiObject.OBJ_STATE_REDRAW & 0xFF);
                            this.container.parent.SendObjectPostrenderEvent(this.container);
                        }
                        /* Draw button frame */
                        if ((this.style & LSB_STYLE_NO_BORDERS) == 0)
                        {
                            if ((this.style & LSB_STYLE_3D) != 0)
                            {  /* 3D */
                                DrawObjectFrame(this.container.a_abs.xs, this.container.a_abs.ys, this.container.a_abs.xe, this.container.a_abs.ye, pal_textbox);
                            }
                            else
                            {  /* 2D */
                                DrawFrame(this.container.a_abs.xs, this.container.a_abs.ys, this.container.a_abs.xe, this.container.a_abs.ye, this.fc);
                            }
                        }
                    }
                    else
                    {
                        if ((this.style & LSB_STYLE_NO_FILL) == 0)
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
