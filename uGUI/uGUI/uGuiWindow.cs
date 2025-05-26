using System;
using System.Collections;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiWindow
        {
            public class uGuiTitle
            {
                public string str { get; set; }
                public uGuiFont font { get; set; }
                public sbyte h_space { get; set; }
                public sbyte v_space { get; set; }
                public byte align { get; set; }
                public uGuiColor fc { get; set; }
                public uGuiColor bc { get; set; }
                public uGuiColor ifc { get; set; }
                public uGuiColor ibc { get; set; }
                public byte height { get; set; }
            }

            /* Window states */
            public const byte WND_STATE_FREE = (1<<0);
            public const byte WND_STATE_VALID = (1<<1);
            public const byte WND_STATE_BUSY = (1<<2);
            public const byte WND_STATE_VISIBLE = (1<<3);
            public const byte WND_STATE_ENABLE = (1<<4);
            public const byte WND_STATE_UPDATE = (1<<5);
            public const byte WND_STATE_REDRAW_TITLE = (1<<6);

            /* Window styles */
            public const byte WND_STYLE_2D = (0<<0);
            public const byte WND_STYLE_3D = (1<<0);
            public const byte WND_STYLE_HIDE_TITLE = (0<<1);
            public const byte WND_STYLE_SHOW_TITLE = (1 << 1);

            public delegate void WindowMessageDelegate(uGuiMessage msg);
            /* -------------------------------------------------------------------------------- */
            /* -- WINDOW                                                                     -- */
            /* -------------------------------------------------------------------------------- */

            internal ArrayList objlst { get; set; }
            internal byte state { get; set; }
            internal uGuiColor fc { get; set; }
            internal uGuiColor bc { get; set; }
            internal Int16 xs { get; set; }
            internal Int16 ys { get; set; }
            internal Int16 xe { get; set; }
            internal Int16 ye { get; set; }
            internal byte style { get; set; }
            internal uGuiTitle title { get; set; }
            internal WindowMessageDelegate cb { get; set; }

            public uGuiWindow(WindowMessageDelegate cb)
            {

                /* Initialize objects list of the window */
                this.objlst = new ArrayList();

                /* Initialize window */
                this.state = WND_STATE_VALID;
                #if  USE_COLOR_RGB565
                    this.fc = new uGuiColor(0x0000);
                    this.bc = new uGuiColor(0xEF7D);
                #else
                    this.fc = new uGuiColor(0x000000);
                    this.bc = new uGuiColor(0xF0F0F0);
                #endif
                this.xs = 0;
                this.ys = 0;
                this.xe = (Int16)(GetXDim() - 1);
                this.ye = (Int16)(GetYDim() - 1);
                this.cb = cb;
                this.style = WND_STYLE_2D | WND_STYLE_SHOW_TITLE;

                /* Initialize window title-bar */
                this.title = new uGuiTitle();
                this.title.str = null;
                if (gui != null)
                {
                    this.title.font = gui.font;
                }
                else
                {
                    this.title.font = null;
                }
                this.title.h_space = 2;
                this.title.v_space = 2;
                this.title.align = ALIGN_CENTER_LEFT;
                this.title.fc = new uGuiColor(uGuiColor.C_WHITE);
                this.title.bc = new uGuiColor(uGuiColor.C_BLUE);
                this.title.ifc = new uGuiColor(uGuiColor.C_WHITE);
                this.title.ibc = new uGuiColor(uGuiColor.C_GRAY);
                this.title.height = 15;
            }

            public sbyte Delete()
            {
                if (this == gui.active_window) return UG_RESULT_FAIL;

                /* Only delete valid windows */
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    this.state = 0;
                    this.cb = null;
                    this.objlst = null;
                    this.xs = 0;
                    this.ys = 0;
                    this.xe = 0;
                    this.ye = 0;
                    this.style = 0;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public void Show()
            {
                /* Force an update, even if this is the active window! */
                this.state |= WND_STATE_VISIBLE | WND_STATE_UPDATE;
                this.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                gui.next_window = this;
            }

            public sbyte Hide()
            {
                if (gui != null)
                {
                    if (this == gui.active_window)
                    {
                        /* Is there an old window which just lost the focus? */
                        if ((gui.last_window != null) && ((gui.last_window.state & WND_STATE_VISIBLE) != 0))
                        {
                            if ((gui.last_window.xs > this.xs) || (gui.last_window.ys > this.ys) || (gui.last_window.xe < this.xe) || (gui.last_window.ye < this.ye))
                            {
                                this.Clear();
                            }
                            gui.next_window = gui.last_window;
                        }
                        else
                        {
                            gui.active_window.state &= (byte)(~WND_STATE_VISIBLE & 0xFF);
                            gui.active_window.state |= WND_STATE_UPDATE;
                        }
                    }
                    else
                    {
                        /* If the old window is visible, clear it! */
                        this.Clear();
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte Resize(Int16 xs, Int16 ys, Int16 xe, Int16 ye)
            {
                Int16 pos;
                Int16 xmax, ymax;

                xmax = (Int16)(GetXDim() - 1);
                ymax = (Int16)(GetYDim() - 1);

                if ((this.state & WND_STATE_VALID) != 0)
                {
                    /* Do some checks... */
                    if ((xs < 0) || (ys < 0)) return UG_RESULT_FAIL;
                    if ((xe > xmax) || (ye > ymax)) return UG_RESULT_FAIL;
                    pos = (Int16)(xe - xs);
                    if (pos < 10)
                    {
                        return UG_RESULT_FAIL;
                    }
                    pos = (Int16)(ye - ys);
                    if (pos < 10)
                    {
                        return UG_RESULT_FAIL;
                    }

                    /* ... and if everything is OK move the window! */
                    this.xs = xs;
                    this.ys = ys;
                    this.xe = xe;
                    this.ye = ye;

                    if (((this.state & WND_STATE_VISIBLE) != 0) && (gui.active_window == this))
                    {
                        if (this.ys != 0)
                        {
                            FillFrame(0, 0, xmax, (Int16)(this.ys - 1), gui.desktop_color);
                        }
                        pos = (Int16)(this.ye + 1);
                        if (!(pos > ymax))
                        {
                            FillFrame(0, pos, xmax, ymax, gui.desktop_color);
                        }
                        if (this.xs != 0)
                        {
                            FillFrame(0, this.ys, (Int16)(this.xs - 1), this.ye, gui.desktop_color);
                        }
                        pos = (Int16)(this.xe + 1);
                        if (!(pos > xmax))
                        {
                            FillFrame(pos, this.ys, xmax, this.ye, gui.desktop_color);
                        }

                        this.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                        this.state |= WND_STATE_UPDATE;
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte Alert()
            {
                uGuiColor c;
                c = this.GetTitleTextColor();
                if (this.SetTitleTextColor(this.GetTitleColor()) == UG_RESULT_FAIL)
                {
                    return UG_RESULT_FAIL;
                }
                if (this.SetTitleColor(c) == UG_RESULT_FAIL)
                {
                    return UG_RESULT_FAIL;
                }
                return UG_RESULT_OK;
            }

            public sbyte SetForeColor(uGuiColor fc)
            {
                if (((this.state & WND_STATE_VALID) != 0) && (fc != null))
                {
                    this.fc = fc;
                    this.state |= WND_STATE_UPDATE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetBackColor(uGuiColor bc)
            {
                if (((this.state & WND_STATE_VALID) != 0) && (bc != null))
                {
                    this.bc = bc;
                    this.state |= WND_STATE_UPDATE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleTextColor(uGuiColor c)
            {
                if ((c != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.fc = c;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleColor(uGuiColor c)
            {
                if ((c != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.bc = c;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleInactiveTextColor(uGuiColor c)
            {
                if ((c != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.ifc = c;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleInactiveColor(uGuiColor c)
            {
                if ((c != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.ibc = c;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleText(string str)
            {
                if ((str != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.str = str;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleTextFont( uGuiFont font )
            {
                if ((font != null) && (this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    this.title.font = font;
                    if ( this.title.height <= (font.char_height + 1) )
                    {
                        this.title.height = (byte)(font.char_height + 2);
                        this.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleTextHSpace(sbyte hs)
            {
                if ((this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.h_space = hs;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleTextVSpace(sbyte vs)
            {
                if ((this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.v_space = vs;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleTextAlignment(byte align)
            {
                if ((this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.align = align;
                    this.state |= WND_STATE_UPDATE | WND_STATE_REDRAW_TITLE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetTitleHeight(byte height)
            {
                if ((this.title != null) && ((this.state & WND_STATE_VALID) != 0))
                {
                    this.title.height = height;
                    this.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                    this.state |= WND_STATE_UPDATE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetXStart(Int16 xs)
            {
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    this.xs = xs;
                    if (this.Resize(this.xs, this.ys, this.xe, this.ye) == UG_RESULT_FAIL)
                    {
                        return UG_RESULT_FAIL;
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetYStart(Int16 ys)
            {
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    this.ys = ys;
                    if (this.Resize(this.xs, this.ys, this.xe, this.ye) == UG_RESULT_FAIL)
                    {
                        return UG_RESULT_FAIL;
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetXEnd(Int16 xe)
            {
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    this.xe = xe;
                    if (this.Resize(this.xs, this.ys, this.xe, this.ye) == UG_RESULT_FAIL)
                    {
                        return UG_RESULT_FAIL;
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetYEnd(Int16 ye)
            {
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    this.ye = ye;
                    if (this.Resize(this.xs, this.ys, this.xe, this.ye) == UG_RESULT_FAIL)
                    {
                        return UG_RESULT_FAIL;
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public sbyte SetStyle(byte style)
            {
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    /* 3D or 2D? */
                    if ((style & WND_STYLE_3D) != 0)
                    {
                        this.style |= WND_STYLE_3D;
                    }
                    else
                    {
                        this.style &= (byte)(~WND_STYLE_3D & 0xFF);
                    }
                    /* Show title-bar? */
                    if ((style & WND_STYLE_SHOW_TITLE) != 0)
                    {
                        this.style |= WND_STYLE_SHOW_TITLE;
                    }
                    else
                    {
                        this.style &= (byte)(~WND_STYLE_SHOW_TITLE & 0xFF);
                    }
                    this.state |= WND_STATE_UPDATE;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public uGuiColor GetForeColor()
            {
                uGuiColor c = null;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    c = this.fc;
                }
                return c;
            }

            public uGuiColor GetBackColor()
            {
                uGuiColor c = null;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    c = this.bc;
                }
                return c;
            }

            public uGuiColor GetTitleTextColor()
            {
                uGuiColor c = null;
                if ((this.title != null) &&(this.state & WND_STATE_VALID) != 0)
                {
                    c = this.title.fc;
                }
                return c;
            }

            public uGuiColor GetTitleColor()
            {
                uGuiColor c = null;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    c = this.title.bc;
                }
                return c;
            }

            public uGuiColor GetTitleInactiveTextColor()
            {
                uGuiColor c = null;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    c = this.title.ifc;
                }
                return c;
            }

            public uGuiColor GetTitleInactiveColor()
            {
                uGuiColor c = null;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    c = this.title.ibc;
                }
                return c;
            }

            public string GetTitleText()
            {
                string str = null;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    str = this.title.str;
                }
                return str;
            }

            public uGuiFont GetTitleTextFont()
            {
                uGuiFont font = null;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    font = this.title.font;
                }
                return font;
            }

            public sbyte GetTitleTextHSpace()
            {
                sbyte hs = 0;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    hs = this.title.h_space;
                }
                return hs;
            }

            public sbyte GetTitleTextVSpace()
            {
                sbyte vs = 0;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    vs = this.title.v_space;
                }
                return vs;
            }

            public byte GetTitleTextAlignment()
            {
                byte align = 0;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    align = this.title.align;
                }
                return align;
            }

            public byte GetTitleHeight()
            {
                byte h = 0;
                if ((this.title != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    h = this.title.height;
                }
                return h;
            }

            public Int16 GetXStart()
            {
                Int16 xs = -1;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    xs = this.xs;
                }
                return xs;
            }

            public Int16 GetYStart()
            {
                Int16 ys = -1;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    ys = this.ys;
                }
                return ys;
            }

            public Int16 GetXEnd()
            {
                Int16 xe = -1;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    xe = this.xe;
                }
                return xe;
            }

            public Int16 GetYEnd()
            {
                Int16 ye = -1;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    ye = this.ye;
                }
                return ye;
            }

            public byte GetStyle()
            {
                byte style = 0;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    style = this.style;
                }
                return style;
            }

            public sbyte GetArea(uGuiArea a)
            {
                if ((a != null) && (this.state & WND_STATE_VALID) != 0)
                {
                    a.xs = this.xs;
                    a.ys = this.ys;
                    a.xe = this.xe;
                    a.ye = this.ye;
                    if ((this.style & WND_STYLE_3D) != 0)
                    {
                        a.xs += 3;
                        a.ys += 3;
                        a.xe -= 3;
                        a.ye -= 3;
                    }
                    if ((this.style & WND_STYLE_SHOW_TITLE) != 0)
                    {
                        a.ys += (Int16)(this.title.height + 1);
                    }
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public Int16 GetInnerWidth()
            {
                Int16 w = 0;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    w = (Int16)(this.xe - this.xs);

                    /* 3D style? */
                    if ((this.style & WND_STYLE_3D) != 0)
                    {
                        w -= 6;
                    }

                    if (w < 0) w = 0;
                }
                return w;
            }

            public Int16 GetOuterWidth()
            {
                Int16 w = 0;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    w = (Int16)(this.xe - this.xs);

                    if (w < 0)
                    {
                        w = 0;
                    }
                }
                return w;
            }

            public Int16 GetInnerHeight()
            {
                Int16 h = 0;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    h = (Int16)(this.ye - this.ys);

                    /* 3D style? */
                    if ((this.style & WND_STYLE_3D) != 0)
                    {
                        h -= 6;
                    }

                    /* Is the title active */
                    if ((this.style & WND_STYLE_SHOW_TITLE) != 0)
                    {
                        h -= this.title.height;
                    }

                    if (h < 0) h = 0;
                }
                return h;
            }

            public Int16 GetOuterHeight()
            {
                Int16 h = 0;
                if ((this.state & WND_STATE_VALID) != 0)
                {
                    h = (Int16)(this.ye - this.ys);

                    if (h < 0)
                    {
                        h = 0;
                    }
                }
                return h;
            }

            public sbyte DrawTitle()
            {
                uGuiText txt = new uGuiText();
                Int16 xs, ys, xe, ye;

                if ((this.state & WND_STATE_VALID) != 0)
                {
                    xs = this.xs;
                    ys = this.ys;
                    xe = this.xe;
                    ye = this.ye;

                    /* 3D style? */
                    if ((this.style & WND_STYLE_3D) != 0)
                    {
                        xs += 3;
                        ys += 3;
                        xe -= 3;
                        ye -= 3;
                    }

                    /* Is the window active or inactive? */
                    if (this == gui.active_window)
                    {
                        txt.bc = this.title.bc;
                        txt.fc = this.title.fc;
                    }
                    else
                    {
                        txt.bc = this.title.ibc;
                        txt.fc = this.title.ifc;
                    }

                    /* Draw title */
                    FillFrame(xs, ys, xe, (Int16)(ys + this.title.height - 1), txt.bc);

                    /* Draw title text */
                    txt.str = this.title.str;
                    txt.font = this.title.font;
                    txt.a = new uGuiArea();
                    txt.a.xs = (Int16)(xs + 3);
                    txt.a.ys = ys;
                    txt.a.xe = xe;
                    txt.a.ye = (Int16)(ys + this.title.height - 1);
                    txt.align = this.title.align;
                    txt.h_space = this.title.h_space;
                    txt.v_space = this.title.v_space;
                    PutText(txt);

                    /* Draw line */
                    DrawLine(xs, (Int16)(ys + this.title.height), xe, (Int16)(ys + this.title.height), pal_window[11]);
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            public void Update()
            {
                Int16 xs, ys, xe, ye;

                xs = this.xs;
                ys = this.ys;
                xe = this.xe;
                ye = this.ye;

                this.state &= (byte)(~WND_STATE_UPDATE & 0xFF);
                /* Is the window visible? */
                if ((this.state & WND_STATE_VISIBLE) != 0)
                {
                    /* 3D style? */
                    if (((this.style & WND_STYLE_3D) != 0) && ((this.state & WND_STATE_REDRAW_TITLE) == 0))
                    {
                        DrawObjectFrame(xs, ys, xe, ye, pal_window);
                        xs += 3;
                        ys += 3;
                        xe -= 3;
                        ye -= 3;
                    }
                    /* Show title bar? */
                    if ((this.style & WND_STYLE_SHOW_TITLE) != 0)
                    {
                        this.DrawTitle();
                        ys += (Int16)(this.title.height + 1);
                        if ((this.state & WND_STATE_REDRAW_TITLE) != 0)
                        {
                            this.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                            return;
                        }
                    }
                    /* Draw window area? */
                    FillFrame(xs, ys, xe, ye, this.bc);

                    /* Force each object to be updated! */
                    if (this.objlst != null)
                    {
                        foreach (uGuiObject obj in this.objlst)
                        {
                            if ((obj != null) && ((obj.state & uGuiObject.OBJ_STATE_FREE) == 0) && ((obj.state & uGuiObject.OBJ_STATE_VALID) != 0) && ((obj.state & uGuiObject.OBJ_STATE_VISIBLE) != 0))
                            {
                                obj.state |= (uGuiObject.OBJ_STATE_UPDATE | uGuiObject.OBJ_STATE_REDRAW);
                            }
                        }
                    }
                }
                else
                {
                    FillFrame(this.xs, this.xs, this.xe, this.ye, gui.desktop_color);
                }
            }

            public sbyte Clear()
            {
                if ((this.state & WND_STATE_VISIBLE) != 0)
                {
                    this.state &= (byte)(~WND_STATE_VISIBLE & 0xFF);
                    FillFrame(this.xs, this.ys, this.xe, this.ye, gui.desktop_color);

                    if (this != gui.active_window)
                    {
                        /* If the current window is visible, update it! */
                        if ((gui.active_window.state & WND_STATE_VISIBLE) != 0)
                        {
                            gui.active_window.state &= (byte)(~WND_STATE_REDRAW_TITLE & 0xFF);
                            gui.active_window.state |= WND_STATE_UPDATE;
                        }
                    }
                }
                return UG_RESULT_OK;
            }

            internal void AddObject(uGuiObject obj)
            {
                if (obj != null)
                {
                    if (this.objlst == null)
                    {
                        this.objlst = new ArrayList();
                    }
                    if (obj.parent == null)
                    {
                        obj.parent = this;
                    }
                    this.objlst.Add(obj);
                }
            }

            public uGuiObject SearchObject(byte type, byte id)
            {
                if (this.objlst != null)
                {
                    foreach (uGuiObject obj in this.objlst)
                    {
                        if (obj != null)
                        {
                            if (((obj.state & uGuiObject.OBJ_STATE_FREE) == 0) && ((obj.state & uGuiObject.OBJ_STATE_VALID) != 0))
                            {
                                if ((obj.type == type) && (obj.id == id))
                                {
                                    /* Requested object found! */
                                    return obj;
                                }
                            }
                        }
                    }
                }
                return null;
            }

            public sbyte DeleteObject( byte type, byte id )
            {
                uGuiObject obj = null;

                obj = this.SearchObject( type, id );

                /* Object found? */
                if ( obj != null )
                {
                    /* We dont't want to delete a visible or busy object! */
                    if (((obj.state & uGuiObject.OBJ_STATE_VISIBLE) != 0) || ((obj.state & uGuiObject.OBJ_STATE_UPDATE) != 0)) 
                    {
                        return UG_RESULT_FAIL;
                    }
                    this.objlst.Remove(obj);
                    obj.state = uGuiObject.OBJ_STATE_INIT;
                    obj.data = null;
                    obj.evt = 0;
                    obj.id = 0;
                    obj.touch_state = 0;
                    obj.type = 0;
                    obj.update = null;
                    return UG_RESULT_OK;
                }
                return UG_RESULT_FAIL;
            }

            internal void ProcessTouchData()
            {
                Int16 xp, yp;
                byte objstate;
                byte objtouch;
                byte tchstate;

                if (gui == null) return;
                if (gui.touch == null) return;
                if (this.objlst == null) return;

                xp = gui.touch.xp;
                yp = gui.touch.yp;
                tchstate = gui.touch.state;
                gui.touch = null;

                foreach (uGuiObject obj in this.objlst)
                {
                    if (obj != null)
                    {
                        objstate = obj.state;
                        objtouch = obj.touch_state;
                        if (((objstate & uGuiObject.OBJ_STATE_FREE) == 0) && ((objstate & uGuiObject.OBJ_STATE_VALID) != 0) && ((objstate & uGuiObject.OBJ_STATE_VISIBLE) != 0) && ((objstate & uGuiObject.OBJ_STATE_REDRAW) == 0))
                        {
                            /* Process touch data */
                            if ((tchstate != 0) && (xp != -1))
                            {
                                if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED) == 0)
                                {
                                    objtouch |= uGuiObject.OBJ_TOUCH_STATE_PRESSED_OUTSIDE_OBJECT | uGuiObject.OBJ_TOUCH_STATE_CHANGED;
                                    objtouch &= (byte)(~(uGuiObject.OBJ_TOUCH_STATE_RELEASED_ON_OBJECT | uGuiObject.OBJ_TOUCH_STATE_RELEASED_OUTSIDE_OBJECT | uGuiObject.OBJ_TOUCH_STATE_CLICK_ON_OBJECT) & 0xFF);
                                }
                                objtouch &= (byte)(~uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED_ON_OBJECT & 0xFF);
                                if (xp >= obj.a_abs.xs)
                                {
                                    if (xp <= obj.a_abs.xe)
                                    {
                                        if (yp >= obj.a_abs.ys)
                                        {
                                            if (yp <= obj.a_abs.ye)
                                            {
                                                objtouch |= uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED_ON_OBJECT;
                                                if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED) == 0)
                                                {
                                                    objtouch &= (byte)(~uGuiObject.OBJ_TOUCH_STATE_PRESSED_OUTSIDE_OBJECT & 0xFF);
                                                    objtouch |= uGuiObject.OBJ_TOUCH_STATE_PRESSED_ON_OBJECT;
                                                }
                                            }
                                        }
                                    }
                                }
                                objtouch |= uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED;
                            }
                            else if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED) != 0)
                            {
                                if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED_ON_OBJECT) != 0)
                                {
                                    if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_PRESSED_ON_OBJECT) != 0)
                                    {
                                        objtouch |= uGuiObject.OBJ_TOUCH_STATE_CLICK_ON_OBJECT;
                                    }
                                    objtouch |= uGuiObject.OBJ_TOUCH_STATE_RELEASED_ON_OBJECT;
                                }
                                else
                                {
                                    objtouch |= uGuiObject.OBJ_TOUCH_STATE_RELEASED_OUTSIDE_OBJECT;
                                }
                                if ((objtouch & uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED) != 0)
                                {
                                    objtouch |= uGuiObject.OBJ_TOUCH_STATE_CHANGED;
                                }
                                objtouch &= (byte)(~(uGuiObject.OBJ_TOUCH_STATE_PRESSED_OUTSIDE_OBJECT | uGuiObject.OBJ_TOUCH_STATE_PRESSED_ON_OBJECT | uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED) & 0xFF);
                            }
                        }
                        obj.touch_state = objtouch;
                    }
                }
            }

            internal void UpdateObjects()
            {
                byte objstate;
                byte objtouch;

                if (this.objlst == null) return;

                /* Check each object, if it needs to be updated? */
                foreach (uGuiObject obj in this.objlst)
                {
                    if (obj != null)
                    {
                        objstate = obj.state;
                        objtouch = obj.touch_state;
                        if (((objstate & uGuiObject.OBJ_STATE_FREE) == 0) && ((objstate & uGuiObject.OBJ_STATE_VALID) != 0))
                        {
                            if ((objstate & uGuiObject.OBJ_STATE_UPDATE) != 0)
                            {
                                if (obj.update != null)
                                {
                                    obj.update();
                                }
                            }
                            if (((objstate & uGuiObject.OBJ_STATE_VISIBLE) != 0) && ((objstate & uGuiObject.OBJ_STATE_TOUCH_ENABLE) != 0))
                            {
                                if ((objtouch & (uGuiObject.OBJ_TOUCH_STATE_CHANGED | uGuiObject.OBJ_TOUCH_STATE_IS_PRESSED)) != 0)
                                {
                                    if (obj.update != null)
                                    {
                                        obj.update();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            internal void HandleEvents()
            {
                byte objstate;

                if (this.objlst == null) return;
                /* Handle window-related events */
                //ToDo


                /* Handle object-related events */
                foreach (uGuiObject obj in this.objlst)
                {
                    if (obj != null)
                    {
                        objstate = obj.state;
                        if (((objstate & uGuiObject.OBJ_STATE_FREE) == 0) && ((objstate & uGuiObject.OBJ_STATE_VALID) != 0))
                        {
                            if (obj.evt != uGuiObject.OBJ_EVENT_NONE)
                            {
                                if (this.cb != null)
                                {
                                    this.cb(new uGuiMessage(uGuiMessage.MSG_TYPE_OBJECT, obj.type, obj.id, obj.evt, obj));
                                }
                                obj.evt = uGuiObject.OBJ_EVENT_NONE;
                            }
                        }
                    }
                }
            }

            internal void SendObjectPrerenderEvent(uGuiObject obj)
            {
                if((this.cb != null) && (obj != null))
                {
                    this.cb(new uGuiMessage(uGuiMessage.MSG_TYPE_OBJECT, obj.type, obj.id, uGuiObject.OBJ_EVENT_PRERENDER, obj));
                }
            }

            internal void SendObjectPostrenderEvent(uGuiObject obj)
            {
                if ((this.cb != null) && (obj != null))
                {
                    this.cb(new uGuiMessage(uGuiMessage.MSG_TYPE_OBJECT, obj.type, obj.id, uGuiObject.OBJ_EVENT_POSTRENDER, obj));
                }
            }
        }
    }
}
