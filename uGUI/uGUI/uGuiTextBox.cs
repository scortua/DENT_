using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiTextBox
        {
            /* Default textbox IDs */
            public const byte TXB_ID_0 = uGuiObject.OBJ_ID_0;
            public const byte TXB_ID_1 = uGuiObject.OBJ_ID_1;
            public const byte TXB_ID_2 = uGuiObject.OBJ_ID_2;
            public const byte TXB_ID_3 = uGuiObject.OBJ_ID_3;
            public const byte TXB_ID_4 = uGuiObject.OBJ_ID_4;
            public const byte TXB_ID_5 = uGuiObject.OBJ_ID_5;
            public const byte TXB_ID_6 = uGuiObject.OBJ_ID_6;
            public const byte TXB_ID_7 = uGuiObject.OBJ_ID_7;
            public const byte TXB_ID_8 = uGuiObject.OBJ_ID_8;
            public const byte TXB_ID_9 = uGuiObject.OBJ_ID_9;
            public const byte TXB_ID_10 = uGuiObject.OBJ_ID_10;
            public const byte TXB_ID_11 = uGuiObject.OBJ_ID_11;
            public const byte TXB_ID_12 = uGuiObject.OBJ_ID_12;
            public const byte TXB_ID_13 = uGuiObject.OBJ_ID_13;
            public const byte TXB_ID_14 = uGuiObject.OBJ_ID_14;
            public const byte TXB_ID_15 = uGuiObject.OBJ_ID_15;
            public const byte TXB_ID_16 = uGuiObject.OBJ_ID_16;
            public const byte TXB_ID_17 = uGuiObject.OBJ_ID_17;
            public const byte TXB_ID_18 = uGuiObject.OBJ_ID_18;
            public const byte TXB_ID_19 = uGuiObject.OBJ_ID_19;
            public const byte TXB_ID_20 = uGuiObject.OBJ_ID_20;
            public const byte TXB_ID_21 = uGuiObject.OBJ_ID_21;
            public const byte TXB_ID_22 = uGuiObject.OBJ_ID_22;
            public const byte TXB_ID_23 = uGuiObject.OBJ_ID_23;
            public const byte TXB_ID_24 = uGuiObject.OBJ_ID_24;
            public const byte TXB_ID_25 = uGuiObject.OBJ_ID_25;
            public const byte TXB_ID_26 = uGuiObject.OBJ_ID_26;
            public const byte TXB_ID_27 = uGuiObject.OBJ_ID_27;
            public const byte TXB_ID_28 = uGuiObject.OBJ_ID_28;
            public const byte TXB_ID_29 = uGuiObject.OBJ_ID_29;
            public const byte TXB_ID_30 = uGuiObject.OBJ_ID_30;
            public const byte TXB_ID_31 = uGuiObject.OBJ_ID_31;
            public const byte TXB_ID_32 = uGuiObject.OBJ_ID_32;
            public const byte TXB_ID_33 = uGuiObject.OBJ_ID_33;
            public const byte TXB_ID_34 = uGuiObject.OBJ_ID_34;
            public const byte TXB_ID_35 = uGuiObject.OBJ_ID_35;
            public const byte TXB_ID_36 = uGuiObject.OBJ_ID_36;
            public const byte TXB_ID_37 = uGuiObject.OBJ_ID_37;
            public const byte TXB_ID_38 = uGuiObject.OBJ_ID_38;
            public const byte TXB_ID_39 = uGuiObject.OBJ_ID_39;
            public const byte TXB_ID_40 = uGuiObject.OBJ_ID_40;
            public const byte TXB_ID_41 = uGuiObject.OBJ_ID_41;
            public const byte TXB_ID_42 = uGuiObject.OBJ_ID_42;
            public const byte TXB_ID_43 = uGuiObject.OBJ_ID_43;
            public const byte TXB_ID_44 = uGuiObject.OBJ_ID_44;
            public const byte TXB_ID_45 = uGuiObject.OBJ_ID_45;
            public const byte TXB_ID_46 = uGuiObject.OBJ_ID_46;
            public const byte TXB_ID_47 = uGuiObject.OBJ_ID_47;
            public const byte TXB_ID_48 = uGuiObject.OBJ_ID_48;
            public const byte TXB_ID_49 = uGuiObject.OBJ_ID_49;
            public const byte TXB_ID_50 = uGuiObject.OBJ_ID_50;


            /* Textbox style */
            public const byte TXB_STYLE_2D = (0 << 0);
            public const byte TXB_STYLE_3D = (1 << 0);
            public const byte TXB_STYLE_TOGGLE_COLORS = (1 << 1);
            public const byte TXB_STYLE_USE_ALTERNATE_COLORS = (1 << 2);
            public const byte TXB_STYLE_NO_BORDERS = (1 << 3);
            public const byte TXB_STYLE_NO_FILL = (1 << 4);

            /* -------------------------------------------------------------------------------- */
            /* -- TEXTBOX OBJECT                                                             -- */
            /* -------------------------------------------------------------------------------- */

            internal string str { get; set; }
            internal uGuiFont font { get; set; }
            internal byte style { get; set; }
            internal uGuiColor fc { get; set; }
            internal uGuiColor bc { get; set; }
            internal byte align { get; set; }
            internal sbyte h_space { get; set; }
            internal sbyte v_space { get; set; }
            internal uGuiObject container { get; set; }

            public uGuiTextBox( uGuiWindow wnd, byte id, Int16 xs, Int16 ys, Int16 xe, Int16 ye )
            {
                if(wnd == null)
                {
                    throw new ArgumentNullException("wnd");
                }
                
                uGuiObject obj = new uGuiObject();

                /* Initialize object-specific parameters */
                this.str = null;
                if (gui != null) 
                {
                    this.font = gui.font;
                }
                else
                {
                    this.font = null;
                }
                this.style = TXB_STYLE_2D; /* reserved */
                this.fc = wnd.fc;
                this.bc = wnd.bc;
                this.align = ALIGN_CENTER;
                this.h_space = 0;
                this.v_space = 0;
                this.container = obj;

                /* Initialize standard object parameters */
                obj.update = this.TextboxUpdate;
                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.type = uGuiObject.OBJ_TYPE_TEXTBOX;
                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.a_rel = new uGuiArea(xs, ys, xe, ye);
                obj.a_abs = new uGuiArea();
                obj.id = id;
                obj.state |= uGuiObject.OBJ_STATE_VISIBLE | uGuiObject.OBJ_STATE_REDRAW | uGuiObject.OBJ_STATE_VALID;
                obj.data = this;

                /* Update function: Do your thing! */
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_FREE & 0xFF);
                wnd.AddObject(obj);
            }
            
            public static sbyte TextboxDelete(uGuiWindow wnd, byte id)
            {
                if (wnd != null)
                {
                    return wnd.DeleteObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                }
                return UG_RESULT_FAIL;
            }
            
            public sbyte Delete()
            {
                return this.container.parent.DeleteObject(uGuiObject.OBJ_TYPE_BUTTON, this.container.id);
            }

            public static sbyte TextboxShow(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
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

            public static sbyte TextboxHide(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;

                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                obj.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public sbyte Hide()
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.container.evt = uGuiObject.OBJ_EVENT_NONE;
                this.container.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public static sbyte TextboxSetForeColor(uGuiWindow wnd, byte id, uGuiColor fc)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (fc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.fc = fc;
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

            public static sbyte TextboxSetBackColor(uGuiWindow wnd, byte id, uGuiColor bc)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (bc == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.bc = bc;
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

            public static sbyte TextboxSetText(uGuiWindow wnd, byte id, string str)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (str == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.str = str;
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

            public static sbyte TextboxSetFont(uGuiWindow wnd, byte id, uGuiFont font)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (font == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.font = font;
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

            public static sbyte TextboxSetStyle(uGuiWindow wnd, byte id, byte style)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                /* Select color scheme */
                txb.style &= (byte)(~(TXB_STYLE_USE_ALTERNATE_COLORS | TXB_STYLE_TOGGLE_COLORS | TXB_STYLE_NO_BORDERS | TXB_STYLE_NO_FILL) & 0xFF);
                //txb.state |= TXB_STATE_ALWAYS_REDRAW;
                if ((style & TXB_STYLE_NO_BORDERS) != 0)
                {
                    txb.style |= TXB_STYLE_NO_BORDERS;
                }
                if ((style & TXB_STYLE_NO_FILL) != 0)
                {
                    txb.style |= TXB_STYLE_NO_FILL;
                }
                if ((style & TXB_STYLE_TOGGLE_COLORS) != 0)
                {
                    txb.style |= TXB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & TXB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    txb.style |= TXB_STYLE_USE_ALTERNATE_COLORS;
                }
                else
                {
                    //txb.state &= (byte)(~BTN_STATE_ALWAYS_REDRAW & 0xFF);
                }

                /* 3D or 2D */
                if ((style & TXB_STYLE_3D) != 0)
                {
                    txb.style |= TXB_STYLE_3D;
                }
                else
                {
                    txb.style &= (byte)(~TXB_STYLE_3D & 0xFF);
                }
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetStyle(byte style)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                /* Select color scheme */
                this.style &= (byte)(~(TXB_STYLE_USE_ALTERNATE_COLORS | TXB_STYLE_TOGGLE_COLORS | TXB_STYLE_NO_BORDERS | TXB_STYLE_NO_FILL) & 0xFF);
                //this.state |= TXB_STATE_ALWAYS_REDRAW;
                if ((style & TXB_STYLE_NO_BORDERS) != 0)
                {
                    this.style |= TXB_STYLE_NO_BORDERS;
                }
                if ((style & TXB_STYLE_NO_FILL) != 0)
                {
                    this.style |= TXB_STYLE_NO_FILL;
                }
                if ((style & TXB_STYLE_TOGGLE_COLORS) != 0)
                {
                    this.style |= TXB_STYLE_TOGGLE_COLORS;
                }
                else if ((style & TXB_STYLE_USE_ALTERNATE_COLORS) != 0)
                {
                    this.style |= TXB_STYLE_USE_ALTERNATE_COLORS;
                }
                else
                {
                    //this.state &= (byte)(~BTN_STATE_ALWAYS_REDRAW & 0xFF);
                }

                /* 3D or 2D */
                if ((style & TXB_STYLE_3D) != 0)
                {
                    this.style |= TXB_STYLE_3D;
                }
                else
                {
                    this.style &= (byte)(~TXB_STYLE_3D & 0xFF);
                }
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public static sbyte TextboxSetHSpace(uGuiWindow wnd, byte id, sbyte hs)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.h_space = hs;
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

            public static sbyte TextboxSetVSpace(uGuiWindow wnd, byte id, sbyte vs)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.v_space = vs;
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

            public static sbyte TextboxSetAlignment(uGuiWindow wnd, byte id, byte align)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                txb = (uGuiTextBox)(obj.data);
                txb.align = align;
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

            public static uGuiColor TextboxGetForeColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                txb = (uGuiTextBox)(obj.data);
                return txb.fc;
            }

            public uGuiColor GetForeColor()
            {
                return this.fc;
            }

            public static uGuiColor TextboxGetBackColor(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_TEXTBOX, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                txb = (uGuiTextBox)(obj.data);
                return txb.bc;
            }

            public uGuiColor GetBackColor()
            {
                return this.bc;
            }

            public static string TextboxGetText(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                txb = (uGuiTextBox)(obj.data);
                return txb.str;
            }

            public string GetText()
            {
                return this.str;
            }

            public static uGuiFont TextboxGetFont(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                txb = (uGuiTextBox)(obj.data);
                return txb.font;
            }

            public uGuiFont GetFont()
            {
                return this.font;
            }

            public static byte TextboxGetStyle(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                txb = (uGuiTextBox)(obj.data);
                return txb.style;
            }

            public byte GetStyle()
            {
                return this.style;
            }

            public static sbyte TextboxGetHSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                txb = (uGuiTextBox)(obj.data);
                return txb.h_space;
            }

            public sbyte GetHSpace()
            {
                return this.h_space;
            }

            public static sbyte TextboxGetVSpace(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                txb = (uGuiTextBox)(obj.data);
                return txb.v_space;
            }

            public sbyte GetVSpace()
            {
                return this.v_space;
            }

            public static byte TextboxGetAlignment(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiTextBox txb = null;

                if (wnd == null) return 0;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_BUTTON, id);
                if (obj == null) return 0;
                if (obj.data == null) return 0;

                txb = (uGuiTextBox)(obj.data);
                return txb.align;
            }

            public byte GetAlignment()
            {
                return this.align;
            }

            private void TextboxUpdate()
            {
                if(this.container == null) return;
                if(this.container.parent == null) return;

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

                if (( this.container.state & uGuiObject.OBJ_STATE_UPDATE ) != 0)
                {
                    if (( this.container.state & uGuiObject.OBJ_STATE_VISIBLE ) != 0)
                    {
                        /* Full redraw necessary? */
                        if (( this.container.state & uGuiObject.OBJ_STATE_REDRAW ) != 0)
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
                            d = (this.style & TXB_STYLE_3D) != 0 ? (byte)3 : (byte)1;

                            txt.bc = this.bc;
                            txt.fc = this.fc;


                            if ((this.style & TXB_STYLE_NO_FILL) == 0)
                            {
                                FillFrame((Int16)(this.container.a_abs.xs + d), (Int16)(this.container.a_abs.ys + d), (Int16)(this.container.a_abs.xe - d), (Int16)(this.container.a_abs.ye - d), txt.bc);
                            }
                            //FillFrame(this.container.a_abs.xs, this.container.a_abs.ys, this.container.a_abs.xe, this.container.a_abs.ye, txt.bc);
                            
                            /* Draw Textbox text */
                            txt.a = new uGuiArea();
                            txt.a.xs = (Int16)(this.container.a_abs.xs + d);
                            txt.a.ys = (Int16)(this.container.a_abs.ys + d);
                            txt.a.xe = (Int16)(this.container.a_abs.xe - d);
                            txt.a.ye = (Int16)(this.container.a_abs.ye - d);
                            txt.align = this.align;
                            txt.font = this.font;
                            txt.h_space = this.h_space;
                            txt.v_space = this.v_space;
                            txt.str = this.str;
                            PutText( txt );
                            this.container.state &= (byte)(~uGuiObject.OBJ_STATE_REDRAW & 0xFF);
                            this.container.parent.SendObjectPostrenderEvent(this.container);
                        }
                        /* Draw button frame */
                        if ((this.style & TXB_STYLE_NO_BORDERS) == 0)
                        {
                            if ((this.style & TXB_STYLE_3D) != 0)
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
                        if ((this.style & TXB_STYLE_NO_FILL) == 0)
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
