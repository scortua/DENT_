using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiImage
        {            
            /* Default image IDs */
            public const byte IMG_ID_0= uGuiObject.OBJ_ID_0;
            public const byte IMG_ID_1= uGuiObject.OBJ_ID_1;
            public const byte IMG_ID_2= uGuiObject.OBJ_ID_2;
            public const byte IMG_ID_3= uGuiObject.OBJ_ID_3;
            public const byte IMG_ID_4= uGuiObject.OBJ_ID_4;
            public const byte IMG_ID_5= uGuiObject.OBJ_ID_5;
            public const byte IMG_ID_6= uGuiObject.OBJ_ID_6;
            public const byte IMG_ID_7= uGuiObject.OBJ_ID_7;
            public const byte IMG_ID_8= uGuiObject.OBJ_ID_8;
            public const byte IMG_ID_9= uGuiObject.OBJ_ID_9;
            public const byte IMG_ID_10= uGuiObject.OBJ_ID_10;
            public const byte IMG_ID_11= uGuiObject.OBJ_ID_11;
            public const byte IMG_ID_12= uGuiObject.OBJ_ID_12;
            public const byte IMG_ID_13= uGuiObject.OBJ_ID_13;
            public const byte IMG_ID_14= uGuiObject.OBJ_ID_14;
            public const byte IMG_ID_15= uGuiObject.OBJ_ID_15;
            public const byte IMG_ID_16= uGuiObject.OBJ_ID_16;
            public const byte IMG_ID_17= uGuiObject.OBJ_ID_17;
            public const byte IMG_ID_18= uGuiObject.OBJ_ID_18;
            public const byte IMG_ID_19 = uGuiObject.OBJ_ID_19;
            public const byte IMG_ID_20 = uGuiObject.OBJ_ID_20;
            public const byte IMG_ID_21 = uGuiObject.OBJ_ID_21;
            public const byte IMG_ID_22 = uGuiObject.OBJ_ID_22;
            public const byte IMG_ID_23 = uGuiObject.OBJ_ID_23;
            public const byte IMG_ID_24 = uGuiObject.OBJ_ID_24;
            public const byte IMG_ID_25 = uGuiObject.OBJ_ID_25;
            public const byte IMG_ID_26 = uGuiObject.OBJ_ID_26;
            public const byte IMG_ID_27 = uGuiObject.OBJ_ID_27;
            public const byte IMG_ID_28 = uGuiObject.OBJ_ID_28;
            public const byte IMG_ID_29 = uGuiObject.OBJ_ID_29;
            public const byte IMG_ID_30 = uGuiObject.OBJ_ID_30;
            public const byte IMG_ID_31 = uGuiObject.OBJ_ID_31;
            public const byte IMG_ID_32 = uGuiObject.OBJ_ID_32;
            public const byte IMG_ID_33 = uGuiObject.OBJ_ID_33;
            public const byte IMG_ID_34 = uGuiObject.OBJ_ID_34;
            public const byte IMG_ID_35 = uGuiObject.OBJ_ID_35;
            public const byte IMG_ID_36 = uGuiObject.OBJ_ID_36;
            public const byte IMG_ID_37 = uGuiObject.OBJ_ID_37;
            public const byte IMG_ID_38 = uGuiObject.OBJ_ID_38;
            public const byte IMG_ID_39 = uGuiObject.OBJ_ID_39;
            public const byte IMG_ID_40 = uGuiObject.OBJ_ID_40;
            public const byte IMG_ID_41 = uGuiObject.OBJ_ID_41;
            public const byte IMG_ID_42 = uGuiObject.OBJ_ID_42;
            public const byte IMG_ID_43 = uGuiObject.OBJ_ID_43;
            public const byte IMG_ID_44 = uGuiObject.OBJ_ID_44;
            public const byte IMG_ID_45 = uGuiObject.OBJ_ID_45;
            public const byte IMG_ID_46 = uGuiObject.OBJ_ID_46;
            public const byte IMG_ID_47 = uGuiObject.OBJ_ID_47;
            public const byte IMG_ID_48 = uGuiObject.OBJ_ID_48;
            public const byte IMG_ID_49 = uGuiObject.OBJ_ID_49;
            public const byte IMG_ID_50 = uGuiObject.OBJ_ID_50;

            /* Image types */
            public const byte IMG_TYPE_BMP = (1<<0);

            /* -------------------------------------------------------------------------------- */
            /* -- IMAGE OBJECT                                                               -- */
            /* -------------------------------------------------------------------------------- */

            internal uGuiBmp img { get; set; }
            internal byte type { get; set; }
            private uGuiObject container { get; set; }

            public uGuiImage( uGuiWindow wnd, byte id, Int16 xs, Int16 ys, Int16 xe, Int16 ye )
            {   
                if(wnd == null)
                {
                    throw new ArgumentNullException("wnd");
                }

                uGuiObject obj = new uGuiObject();

                this.img = null;
                this.type = IMG_TYPE_BMP;
                this.container = obj;

                /* Initialize standard object parameters */
                obj.update = ImageUpdate;
                obj.touch_state = uGuiObject.OBJ_TOUCH_STATE_INIT;
                obj.type = uGuiObject.OBJ_TYPE_IMAGE;
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
            
            public static sbyte ImageDelete(uGuiWindow wnd, byte id)
            {
                if (wnd != null)
                {
                    return wnd.DeleteObject(uGuiObject.OBJ_TYPE_IMAGE, id);
                }
                return UG_RESULT_FAIL;
            }

            public sbyte Delete()
            {
                return this.container.parent.DeleteObject(uGuiObject.OBJ_TYPE_IMAGE, this.container.id);
            }

            public static sbyte ImageShow(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_IMAGE, id);
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

            public static sbyte ImageHide(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;

                if (wnd == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_IMAGE, id);
                if (obj == null) return UG_RESULT_FAIL;
                obj.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                obj.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public sbyte Hide()
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.container.state &= (byte)(~uGuiObject.OBJ_STATE_VISIBLE & 0xFF);
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE;

                return UG_RESULT_OK;
            }

            public static sbyte ImageSetBmp(uGuiWindow wnd, byte id, uGuiBmp bmp)
            {
                uGuiObject obj = null;
                uGuiImage img = null;

                if (wnd == null) return UG_RESULT_FAIL;
                if (bmp == null) return UG_RESULT_FAIL;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_IMAGE, id);
                if (obj == null) return UG_RESULT_FAIL;
                if (obj.data == null) return UG_RESULT_FAIL;

                img = (uGuiImage)(obj.data);
                img.img = bmp;
                obj.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }

            public sbyte SetBmp(uGuiBmp bmp)
            {
                if (this.container == null) return UG_RESULT_FAIL;

                this.img = bmp;
                this.container.state |= uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW;

                return UG_RESULT_OK;
            }
            
            public static uGuiBmp ImageGetBmp(uGuiWindow wnd, byte id)
            {
                uGuiObject obj = null;
                uGuiImage img = null;

                if (wnd == null) return null;
                obj = wnd.SearchObject(uGuiObject.OBJ_TYPE_IMAGE, id);
                if (obj == null) return null;
                if (obj.data == null) return null;

                img = (uGuiImage)(obj.data);

                return img.img;
            }

            public uGuiBmp GetBmp()
            {
                return this.img;
            }

            private void ImageUpdate()
            {
                if (this.container == null) return;
                if (this.container.parent == null) return;
                

                uGuiArea a = new uGuiArea();


                /* -------------------------------------------------- */
                /* Object touch section                               */
                /* -------------------------------------------------- */

                /* Image doesn't support touch */

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

                            if (this.img != null)
                            {
                                this.container.parent.GetArea(a);
                                /* ToDo: more/better image features */
                                this.container.a_abs.xs = (Int16)(this.container.a_rel.xs + a.xs);
                                this.container.a_abs.ys = (Int16)(this.container.a_rel.ys + a.ys);
                                this.container.a_abs.xe = (Int16)(this.container.a_rel.xs + this.img.width + a.xs);
                                this.container.a_abs.ye = (Int16)(this.container.a_rel.ys + this.img.height + a.ys);
                                if (this.container.a_abs.ye >= this.container.parent.ye) return;
                                if (this.container.a_abs.xe >= this.container.parent.xe) return;

                                /* Draw Image */
                                if ((this.type & IMG_TYPE_BMP) != 0)
                                {
                                    DrawBMP(this.container.a_abs.xs, this.container.a_abs.ys, this.img);
                                }
                            }

                            this.container.state &= (byte)(~uGuiObject.OBJ_STATE_REDRAW & 0xFF);
                        }
                    }
                    else
                    {
                        FillFrame(this.container.a_abs.xs, this.container.a_abs.ys, this.container.a_abs.xe, this.container.a_abs.ye, this.container.parent.bc);
                    }
                    this.container.state &= (byte)(~uGuiObject.OBJ_STATE_UPDATE & 0xFF);
                }
            }
        }
    }
}
