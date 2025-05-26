using System;
using System.Collections;
using System.Threading;

namespace uGUI
{
    public partial class uGui
    {
        private static uGui gui = null;

        public delegate void PixelSetDelegate(Int16 x, Int16 y, uGuiColor color);

        public const byte UG_SATUS_WAIT_FOR_UPDATE = (1 << 0);

        /* Alignments */
        public const byte ALIGN_H_LEFT = (1<<0);
        public const byte ALIGN_H_CENTER = (1<<1);
        public const byte ALIGN_H_RIGHT = (1<<2);
        public const byte ALIGN_V_TOP = (1<<3);
        public const byte ALIGN_V_CENTER = (1<<4);
        public const byte ALIGN_V_BOTTOM = (1<<5);
        public const byte ALIGN_BOTTOM_RIGHT = (ALIGN_V_BOTTOM|ALIGN_H_RIGHT);
        public const byte ALIGN_BOTTOM_CENTER = (ALIGN_V_BOTTOM|ALIGN_H_CENTER);
        public const byte ALIGN_BOTTOM_LEFT = (ALIGN_V_BOTTOM|ALIGN_H_LEFT);
        public const byte ALIGN_CENTER_RIGHT = (ALIGN_V_CENTER|ALIGN_H_RIGHT);
        public const byte ALIGN_CENTER = (ALIGN_V_CENTER|ALIGN_H_CENTER);
        public const byte ALIGN_CENTER_LEFT = (ALIGN_V_CENTER|ALIGN_H_LEFT);
        public const byte ALIGN_TOP_RIGHT = (ALIGN_V_TOP|ALIGN_H_RIGHT);
        public const byte ALIGN_TOP_CENTER = (ALIGN_V_TOP|ALIGN_H_CENTER);
        public const byte ALIGN_TOP_LEFT = (ALIGN_V_TOP | ALIGN_H_LEFT);
        
        /* -------------------------------------------------------------------------------- */
        /* -- FUNCTION RESULTS                                                           -- */
        /* -------------------------------------------------------------------------------- */
        public const sbyte UG_RESULT_FAIL = -1;
        public const sbyte UG_RESULT_OK = 0;


#if !USE_COLOR_RGB565

        private static readonly UInt32[] col_window =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xFFFFFF,
           0xFFFFFF,
           0x696969,
           0x696969,
           /* Frame 2 */
           0xE3E3E3,
           0xE3E3E3,
           0xA0A0A0,
           0xA0A0A0,
        };

        private static readonly UInt32[] col_button_pressed =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           /* Frame 2 */
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
        };

        private static readonly UInt32[] col_button_released =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xFFFFFF,
           0xFFFFFF,
           0x696969,
           0x696969,
           /* Frame 2 */
           0xE3E3E3,
           0xE3E3E3,
           0xA0A0A0,
           0xA0A0A0,
        };

        private static readonly UInt32[] col_checkbox_pressed =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           /* Frame 2 */
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
        };

        private static readonly UInt32[] col_checkbox_released =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xFFFFFF,
           0xFFFFFF,
           0x696969,
           0x696969,
           /* Frame 2 */
           0xE3E3E3,
           0xE3E3E3,
           0xA0A0A0,
           0xA0A0A0,
        };



        private static readonly UInt32[] col_textbox =
        {
           /* Frame 0 */
           0x646464,
           0x646464,
           0x646464,
           0x646464,
           /* Frame 1 */
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           0xA0A0A0,
           /* Frame 2 */
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
           0xF0F0F0,
        };
#endif

#if USE_COLOR_RGB565
        private static readonly UInt16[] col_window =
        {
           0x632C,
           0x632C,
           0x632C,
           0x632C,

           0xFFFF,
           0xFFFF,
           0x6B4D,
           0x6B4D,

           0xE71C,
           0xE71C,
           0x9D13,
           0x9D13,
        };

        private static readonly UInt16[] col_button_pressed =
        {
            0x632C,
            0x632C,
            0x632C,
            0x632C,

            0x9D13,
            0x9D13,
            0x9D13,
            0x9D13,

            0xEF7D,
            0xEF7D,
            0xEF7D,
            0xEF7D,
        };

        private static readonly UInt16[] col_button_released =
        {
            0x632C,
            0x632C,
            0x632C,
            0x632C,

            0xFFFF,
            0xFFFF,
            0x6B4D,
            0x6B4D,

            0xE71C,
            0xE71C,
            0x9D13,
            0x9D13,
        };

        private static readonly UInt16[] col_checkbox_pressed =
        {
            0x632C,
            0x632C,
            0x632C,
            0x632C,

            0x9D13,
            0x9D13,
            0x9D13,
            0x9D13,

            0xEF7D,
            0xEF7D,
            0xEF7D,
            0xEF7D,
        };

        private static readonly UInt16[] col_checkbox_released =
        {
            0x632C,
            0x632C,
            0x632C,
            0x632C,

            0xFFFF,
            0xFFFF,
            0x6B4D,
            0x6B4D,

            0xE71C,
            0xE71C,
            0x9D13,
            0x9D13,
        };
#endif

        private static readonly uGuiColor[] pal_window = new uGuiColor[col_window.Length];
        private static readonly uGuiColor[] pal_button_pressed = new uGuiColor[col_button_pressed.Length];
        private static readonly uGuiColor[] pal_button_released = new uGuiColor[col_button_released.Length];
        private static readonly uGuiColor[] pal_checkbox_pressed = new uGuiColor[col_checkbox_pressed.Length];
        private static readonly uGuiColor[] pal_checkbox_released = new uGuiColor[col_checkbox_released.Length];
        private static readonly uGuiColor[] pal_textbox = new uGuiColor[col_textbox.Length];

/* -------------------------------------------------------------------------------- */
        /* -- µGUI CORE STRUCTURE                                                        -- */
        /* -------------------------------------------------------------------------------- */

        private PixelSetDelegate pset;
        private Int16 x_dim;
        private Int16 y_dim;
        private uGuiTouch touch;
        private uGuiWindow next_window;
        public uGuiWindow active_window { get; protected set; }
        private uGuiWindow last_window;
        private uGuiConsole console;
        private uGuiFont font;
        private sbyte char_h_space;
        private sbyte char_v_space;
        private uGuiColor fore_color;
        private uGuiColor back_color;
        private uGuiColor desktop_color;
        private byte state;
        private uGuiDriver[] driver;

        public uGui(PixelSetDelegate pset, Int16 x, Int16 y)
        {
            this.pset = pset;
            this.x_dim = x;
            this.y_dim = y;
            this.console = new uGuiConsole();
            this.console.x_start = 4;
            this.console.y_start = 4;
            this.console.x_end = (Int16)(this.x_dim - this.console.x_start - 1);
            this.console.y_end = (Int16)(this.y_dim - this.console.x_start - 1);
            this.console.x_pos = this.console.x_end;
            this.console.y_pos = this.console.y_end;
            this.char_h_space = 1;
            this.char_v_space = 1;
            this.font = null;
            //this.font = new uGuiFont(null, 0, 0, 0, 0, null);
#if USE_COLOR_RGB565
            //this.desktop_color = new uGuiColor(0x5C5D);
#else
            this.desktop_color = new uGuiColor(0x5E8BEf);
#endif
            this.fore_color = new uGuiColor(uGuiColor.C_WHITE);
            this.back_color = new uGuiColor(uGuiColor.C_BLACK);
            this.next_window = null;
            this.active_window = null;
            this.last_window = null;
            this.touch = null;

            /* Clear drivers */
            this.driver = new uGuiDriver[uGuiDriver.NUMBER_OF_DRIVERS];
            for (int i = 0; i < this.driver.Length; i++)
            {
                this.driver[i] = new uGuiDriver();
            }
            gui = this;
        }

        public static void InitColorPalete()
        {
            try
            {
                for (int index = 0; index < pal_window.Length; index++)
                {
                    pal_window[index] = new uGuiColor(col_window[index]);
                }
            }
            catch { }
            try
            {
                for (int index = 0; index < pal_button_pressed.Length; index++)
                {
                    pal_button_pressed[index] = new uGuiColor(col_button_pressed[index]);
                }
            }
            catch { }
            try
            {
                for (int index = 0; index < pal_button_released.Length; index++)
                {
                    pal_button_released[index] = new uGuiColor(col_button_released[index]);
                }
            }
            catch { }
            try
            {
                for (int index = 0; index < pal_checkbox_pressed.Length; index++)
                {
                    pal_checkbox_pressed[index] = new uGuiColor(col_checkbox_pressed[index]);
                }
            }
            catch { }
            try
            {
                for (int index = 0; index < pal_checkbox_released.Length; index++)
                {
                    pal_checkbox_released[index] = new uGuiColor(col_checkbox_released[index]);
                }
            }
            catch { }
        }

        public static uGui SelectGUI
        {
            set
            {
                gui = value;
            }
        }

        public uGuiFont FontSelect
        {
            set
            {
                this.font = value;
            }
        }

        public static void FillScreen(uGuiColor c)
        {
            FillFrame(0, 0, (Int16)(gui.x_dim - 1), (Int16)(gui.y_dim - 1), c);
        }

        public static void FillFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            Int16 n,m;

            if (gui == null)
            {
                return;
            }

            if ( x2 < x1 )
            {
                n = x2;
                x2 = x1;
                x1 = n;
            }
            if ( y2 < y1 )
            {
                n = y2;
                y2 = y1;
                y1 = n;
            }

            /* Is hardware acceleration available? */
            if(gui.driver != null)
            {
                if (( gui.driver[uGuiDriver.DRIVER_FILL_FRAME].state & uGuiDriver.DRIVER_ENABLED ) != 0)
                {
                    if(gui.driver[uGuiDriver.DRIVER_FILL_FRAME].driver != null)
                    {
                        if (gui.driver[uGuiDriver.DRIVER_FILL_FRAME].driver(x1, y1, x2, y2, c) == UG_RESULT_OK)
                        {
                            return;
                        }
                    }
                }
            }

            for( m=y1; m<=y2; m++ )
            {
                for( n=x1; n<=x2; n++ )
                {
                    if(gui.pset != null)
                    {
                        gui.pset(n,m,c);
                    }
                }
            }
        }

        public static void FillRoundFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, Int16 r, uGuiColor c)
        {
            Int16 x, y, xd;

            if (x2 < x1)
            {
                x = x2;
                x2 = x1;
                x1 = x;
            }
            if (y2 < y1)
            {
                y = y2;
                y2 = y1;
                y1 = y;
            }

            if (r <= 0) return;

            xd = (Int16)(3 - (r << 1));
            x = 0;
            y = r;

            FillFrame((Int16)(x1 + r), y1, (Int16)(x2 - r), y2, c);

            while (x <= y)
            {
                if (y > 0)
                {
                    DrawLine((Int16)(x2 + x - r), (Int16)(y1 - y + r), (Int16)(x2 + x - r), (Int16)(y + y2 - r), c);
                    DrawLine((Int16)(x1 - x + r), (Int16)(y1 - y + r), (Int16)(x1 - x + r), (Int16)(y + y2 - r), c);
                }
                if (x > 0)
                {
                    DrawLine((Int16)(x1 - y + r), (Int16)(y1 - x + r), (Int16)(x1 - y + r), (Int16)(x + y2 - r), c);
                    DrawLine((Int16)(x2 + y - r), (Int16)(y1 - x + r), (Int16)(x2 + y - r), (Int16)(x + y2 - r), c);
                }
                if (xd < 0)
                {
                    xd += (Int16)((x << 2) + 6);
                }
                else
                {
                    xd += (Int16)(((x - y) << 2) + 10);
                    y--;
                }
                x++;
            }
        }

        public static void DrawMesh(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            Int16 n, m;

            if (gui == null)
            {
                return;
            }

            if (x2 < x1)
            {
                n = x2;
                x2 = x1;
                x1 = n;
            }
            if (y2 < y1)
            {
                n = y2;
                y2 = y1;
                y1 = n;
            }

            for (m = y1; m <= y2; m += 2)
            {
                for (n = x1; n <= x2; n += 2)
                {
                    if (gui.pset != null)
                    {
                        gui.pset(n, m, c);
                    }
                }
            }
        }

        public static void DrawFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            DrawLine(x1, y1, x2, y1, c);
            DrawLine(x1, y2, x2, y2, c);
            DrawLine(x1, y1, x1, y2, c);
            DrawLine(x2, y1, x2, y2, c);
        }

        public static void DrawRoundFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, Int16 r, uGuiColor c)
        {
            Int16 n;
            if (x2 < x1)
            {
                n = x2;
                x2 = x1;
                x1 = n;
            }
            if (y2 < y1)
            {
                n = y2;
                y2 = y1;
                y1 = n;
            }

            if (r > x2) return;
            if (r > y2) return;

            DrawLine((Int16)(x1 + r), y1, (Int16)(x2 - r), y1, c);
            DrawLine((Int16)(x1 + r), y2, (Int16)(x2 - r), y2, c);
            DrawLine(x1, (Int16)(y1 + r), x1, (Int16)(y2 - r), c);
            DrawLine(x2, (Int16)(y1 + r), x2, (Int16)(y2 - r), c);
            DrawArc((Int16)(x1 + r), (Int16)(y1 + r), r, 0x0C, c);
            DrawArc((Int16)(x2 - r), (Int16)(y1 + r), r, 0x03, c);
            DrawArc((Int16)(x1 + r), (Int16)(y2 - r), r, 0x30, c);
            DrawArc((Int16)(x2 - r), (Int16)(y2 - r), r, 0xC0, c);
        }

        public static void DrawPixel(Int16 x0, Int16 y0, uGuiColor c)
        {
            if (gui != null)
            {
                if (gui.pset != null)
                {
                    gui.pset(x0, y0, c);
                }
            }
        }

        public static void DrawCircle(Int16 x0, Int16 y0, Int16 r, uGuiColor c)
        {
            Int16 x, y, xd, yd, e;

            if (gui == null) return;
            if (x0 < 0) return;
            if (y0 < 0) return;
            if (r <= 0) return;

            xd = (Int16)(1 - (r << 1));
            yd = 0;
            e = 0;
            x = r;
            y = 0;

            while (x >= y)
            {
                if (gui.pset != null)
                {
                    gui.pset((Int16)(x0 - x), (Int16)(y0 + y), c);
                    gui.pset((Int16)(x0 - x), (Int16)(y0 - y), c);
                    gui.pset((Int16)(x0 + x), (Int16)(y0 + y), c);
                    gui.pset((Int16)(x0 + x), (Int16)(y0 - y), c);
                    gui.pset((Int16)(x0 - y), (Int16)(y0 + x), c);
                    gui.pset((Int16)(x0 - y), (Int16)(y0 - x), c);
                    gui.pset((Int16)(x0 + y), (Int16)(y0 + x), c);
                    gui.pset((Int16)(x0 + y), (Int16)(y0 - x), c);
                }

                y++;
                e += yd;
                yd += 2;
                if (((e << 1) + xd) > 0)
                {
                    x--;
                    e += xd;
                    xd += 2;
                }
            }
        }

        public static void FillCircle(Int16 x0, Int16 y0, Int16 r, uGuiColor c)
        {
            Int16 x, y, xd;

            if (x0 < 0) return;
            if (y0 < 0) return;
            if (r <= 0) return;

            xd = (Int16)(3 - (r << 1));
            x = 0;
            y = r;

            while (x <= y)
            {
                if (y > 0)
                {
                    DrawLine((Int16)(x0 - x), (Int16)(y0 - y), (Int16)(x0 - x), (Int16)(y0 + y), c);
                    DrawLine((Int16)(x0 + x), (Int16)(y0 - y), (Int16)(x0 + x), (Int16)(y0 + y), c);
                }
                if (x > 0)
                {
                    DrawLine((Int16)(x0 - y), (Int16)(y0 - x), (Int16)(x0 - y), (Int16)(y0 + x), c);
                    DrawLine((Int16)(x0 + y), (Int16)(y0 - x), (Int16)(x0 + y), (Int16)(y0 + x), c);
                }
                if (xd < 0)
                {
                    xd += (Int16)((x << 2) + 6);
                }
                else
                {
                    xd += (Int16)(((x - y) << 2) + 10);
                    y--;
                }
                x++;
            }
            DrawCircle(x0, y0, r, c);
        }

        public static void DrawArc(Int16 x0, Int16 y0, Int16 r, byte s, uGuiColor c)
        {
            Int16 x, y, xd, yd, e;

            if (gui == null) return;
            if (x0 < 0) return;
            if (y0 < 0) return;
            if (r <= 0) return;

            xd = (Int16)(1 - (r << 1));
            yd = 0;
            e = 0;
            x = r;
            y = 0;

            while (x >= y)
            {
                if (gui.pset != null)
                {
                    // Q1
                    if ((s & 0x01) != 0) gui.pset((Int16)(x0 + x), (Int16)(y0 - y), c);
                    if ((s & 0x02) != 0) gui.pset((Int16)(x0 + y), (Int16)(y0 - x), c);

                    // Q2
                    if ((s & 0x04) != 0) gui.pset((Int16)(x0 - y), (Int16)(y0 - x), c);
                    if ((s & 0x08) != 0) gui.pset((Int16)(x0 - x), (Int16)(y0 - y), c);

                    // Q3
                    if ((s & 0x10) != 0) gui.pset((Int16)(x0 - x), (Int16)(y0 + y), c);
                    if ((s & 0x20) != 0) gui.pset((Int16)(x0 - y), (Int16)(y0 + x), c);

                    // Q4
                    if ((s & 0x40) != 0) gui.pset((Int16)(x0 + y), (Int16)(y0 + x), c);
                    if ((s & 0x80) != 0) gui.pset((Int16)(x0 + x), (Int16)(y0 + y), c);
                }

                y++;
                e += yd;
                yd += 2;
                if (((e << 1) + xd) > 0)
                {
                    x--;
                    e += xd;
                    xd += 2;
                }
            }
        }

        public static void DrawLine(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            Int16 n, dx, dy, sgndx, sgndy, dxabs, dyabs, x, y, drawx, drawy;

            if (gui == null)
            {
                return;
            }

            if (x2 < x1)
            {
                n = x2;
                x2 = x1;
                x1 = n;
            }
            if (y2 < y1)
            {
                n = y2;
                y2 = y1;
                y1 = n;
            }

            /* Is hardware acceleration available? */
            if (gui.driver != null)
            {
                if ((gui.driver[uGuiDriver.DRIVER_DRAW_LINE].state & uGuiDriver.DRIVER_ENABLED) != 0)
                {
                    if (gui.driver[uGuiDriver.DRIVER_DRAW_LINE].driver != null)
                    {
                        if ((gui.driver[uGuiDriver.DRIVER_DRAW_LINE].driver)(x1, y1, x2, y2, c) == UG_RESULT_OK)
                        {
                            return;
                        }
                    }
                }
            }

            dx = (Int16)(x2 - x1);
            dy = (Int16)(y2 - y1);
            dxabs = (Int16)((dx > 0) ? dx : -dx);
            dyabs = (Int16)((dy > 0) ? dy : -dy);
            sgndx = (Int16)((dx > 0) ? 1 : -1);
            sgndy = (Int16)((dy > 0) ? 1 : -1);
            x = (Int16)(dyabs >> 1);
            y = (Int16)(dxabs >> 1);
            drawx = x1;
            drawy = y1;

            if (gui.pset != null)
            {
                gui.pset(drawx, drawy, c);
            }

            if (dxabs >= dyabs)
            {
                for (n = 0; n < dxabs; n++)
                {
                    y += dyabs;
                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        drawy += sgndy;
                    }
                    drawx += sgndx;
                    if (gui.pset != null)
                    {
                        gui.pset(drawx, drawy, c);
                    }
                }
            }
            else
            {
                for (n = 0; n < dyabs; n++)
                {
                    x += dxabs;
                    if (x >= dyabs)
                    {
                        x -= dyabs;
                        drawx += sgndx;
                    }
                    drawy += sgndy;
                    if (gui.pset != null)
                    {
                        gui.pset(drawx, drawy, c);
                    }
                }
            }
        }

        public static void PutString(Int16 x, Int16 y, string str)
        {
            Int16 xp, yp;
            byte cw;

            if (gui == null) return;
            if (str == null) return;
            if (gui.font == null) return;

            xp = x;
            yp = y;

            foreach (char chr in str)
            {
                if (chr < gui.font.start_char || chr > gui.font.end_char) continue;
                if (chr == '\n')
                {
                    xp = gui.x_dim;
                    continue;
                }
                cw = gui.font.widths != null ? gui.font.widths[chr - gui.font.start_char] : (byte)(gui.font.char_width);

                if (xp + cw > gui.x_dim - 1)
                {
                    xp = x;
                    yp += (Int16)(gui.font.char_height + gui.char_v_space);
                }

                PutChar(chr, xp, yp, gui.fore_color, gui.back_color);

                xp += (Int16)(cw + gui.char_h_space);
            }
        }

        public static void PutChar(char chr, Int16 x, Int16 y, uGuiColor fc, uGuiColor bc)
        {
            if ((gui != null) && (gui.font != null))
            {
                PutChar(chr, x, y, fc, bc, gui.font);
            }
        }
                
        public static void ConsolePutString(string str)
        {
            byte cw;
            if (str == null) return;
            if (gui == null) return;
            if (gui.font == null) return;
            if (gui.console == null) return;
            foreach(char chr in str)
            {
                if (chr == '\n')
                {
                    gui.console.x_pos = gui.x_dim;
                    continue;
                }

                cw = gui.font.widths != null ? gui.font.widths[chr - gui.font.start_char] : (byte)gui.font.char_width;
                gui.console.x_pos += (Int16)(cw + gui.char_h_space);

                if ((Int16)(gui.console.x_pos + cw) > gui.console.x_end)
                {
                    gui.console.x_pos = gui.console.x_start;
                    gui.console.y_pos += (Int16)(gui.font.char_height + gui.char_v_space);
                }
                if (gui.console.y_pos + gui.font.char_height > gui.console.y_end)
                {
                    gui.console.x_pos = gui.console.x_start;
                    gui.console.y_pos = gui.console.y_start;
                    FillFrame(gui.console.x_start, gui.console.y_start, gui.console.x_end, gui.console.y_end, gui.console.back_color);
                }

                PutChar(chr, gui.console.x_pos, gui.console.y_pos, gui.console.fore_color, gui.console.back_color);
            }
        }

        public static void ConsoleDrawArea()
        {
            if (gui == null) return;
            if (gui.console == null) return;
            FillFrame(gui.console.x_start, gui.console.y_start, gui.console.x_end, gui.console.y_end, gui.console.back_color);
        }

        public static void ConsoleSetArea(Int16 xs, Int16 ys, Int16 xe, Int16 ye)
        {
            if (gui == null) return;
            if (gui.console == null) return;
            gui.console.x_start = xs;
            gui.console.y_start = ys;
            gui.console.x_end = xe;
            gui.console.y_end = ye;
        }

        public static void ConsoleSetForecolor(uGuiColor c)
        {
            if (c == null) return;
            if (gui == null) return;
            if (gui.console == null) return;
            gui.console.fore_color = c;
        }

        public static void ConsoleSetBackcolor(uGuiColor c)
        {
            if (c == null) return;
            if (gui == null) return;
            if (gui.console == null) return;
            gui.console.back_color = c;
        }

        public static void SetForecolor(uGuiColor c)
        {
            if (c == null) return;
            if (gui == null) return;
            gui.fore_color = c;
        }

        public static void SetBackcolor(uGuiColor c)
        {
            if (c == null) return;
            if (gui == null) return;
            gui.back_color = c;
        }

        public static Int16 GetXDim()
        {
            if (gui == null) return 0;
            return gui.x_dim;
        }

        public static Int16 GetYDim()
        {
            if (gui == null) return 0;
            return gui.y_dim;
        }

        public static void FontSetHSpace(sbyte s)
        {
            if (gui == null) return;
            gui.char_h_space = s;
        }

        public static void FontSetVSpace(sbyte s)
        {
            if (gui == null) return;
            gui.char_v_space = s;
        }

        public static int GetPixelsStringLength(string str, Int16 char_width, Int16 h_space = 1)
        {
            int res = 0;
            if (str != null)
            {
                res = str.Length * char_width;
                if (str.Length > 0)
                {
                    res += (str.Length - 1) * h_space;
                }
            }
            return res;
        }

        public static int GetPixelsStringLength(string str, uGuiFont font, Int16 h_space = 1)
        {
            int res = 0;
            if ((str != null) && (font != null))
            {
                res = str.Length * font.char_width;
                if (str.Length > 0)
                {
                    res += (str.Length - 1) * h_space;
                }
            }
            return res;
        }

        private static void PutChar(char chr, Int16 x, Int16 y, uGuiColor fc, uGuiColor bc, uGuiFont font)
        {

            UInt16 i, j, k, xo, yo, c, bn, actual_char_width;
            byte b, bt;
            UInt32 index;
            uGuiColor color = new uGuiColor();

            if (gui == null) return;

            bt = (byte)chr;

            switch (bt)
            {
                case 0xF6: bt = 0x94; break; // ö
                case 0xD6: bt = 0x99; break; // Ö
                case 0xFC: bt = 0x81; break; // ü
                case 0xDC: bt = 0x9A; break; // Ü
                case 0xE4: bt = 0x84; break; // ä
                case 0xC4: bt = 0x8E; break; // Ä
                case 0xB5: bt = 0xE6; break; // µ
                case 0xB0: bt = 0xF8; break; // °
            }

            if (bt < font.start_char || bt > font.end_char) return;

            yo = (UInt16)y;
            bn = (UInt16)font.char_width;
            if (bn == 0) return;
            bn >>= 3;
            if ((font.char_width % 8) != 0) bn++;
            actual_char_width = (font.widths != null ? font.widths[bt - font.start_char] : (UInt16)font.char_width);

            /* Is hardware acceleration available? */
            if ((gui.driver != null) && ((gui.driver[uGuiDriver.DRIVER_FILL_AREA].state & uGuiDriver.DRIVER_ENABLED) != 0) && (gui.driver[uGuiDriver.DRIVER_FILL_AREA].driver != null))
            {
                if (font.font_type == uGuiFont.FONT_TYPE.FONT_TYPE_1BPP)
                {
                    index = (uint)((bt - font.start_char) * font.char_height * bn);
                    gui.driver[uGuiDriver.DRIVER_FILL_AREA].driver(x, y, (Int16)(x + actual_char_width - 1), (Int16)(y + font.char_height - 1), bc);
                    for (j = 0; j < font.char_height; j++)
                    {
                        xo = (UInt16)x;
                        c = actual_char_width;
                        for (i = 0; i < bn; i++)
                        {
                            //b = font.p[(bt - font.start_char) * (j * bn) + i];
                            b = font.p[index++];
                            for (k = 0; (k < 8) && (c != 0); k++)
                            {
                                if ((b & 0x01) != 0)
                                {
                                    //gui.driver[uGuiDriver.DRIVER_FILL_AREA].driver(x, y, (Int16)(x + actual_char_width - 1), (Int16)(y + font.char_height - 1), fc);
                                    gui.pset((Int16)xo, (Int16)yo, fc);
                                }
                                /*else
                                {
                                    gui.driver[uGuiDriver.DRIVER_FILL_AREA].driver(x, y, (Int16)(x + actual_char_width - 1), (Int16)(y + font.char_height - 1), bc);
                                }*/
                                xo++;
                                b >>= 1;
                                c--;
                            }
                        }
                        yo++;
                    }
                }
                else if (font.font_type == uGuiFont.FONT_TYPE.FONT_TYPE_8BPP)
                {
                    index = (uint)((bt - font.start_char) * font.char_height * bn);
                    for (j = 0; j < font.char_height; j++)
                    {
                        for (i = 0; i < actual_char_width; i++)
                        {
                            //b = font.p[(bt - font.start_char) * (j * bn) + i];
                            b = font.p[index++];
                            color.color = (UInt32)(((((fc.color & 0xFF) * b + (bc.color & 0xFF) * (256 - b)) >> 8) & 0xFF) |//Blue component
                                                  ((((fc.color & 0xFF00) * b + (bc.color & 0xFF00) * (256 - b)) >> 8) & 0xFF00) |//Green component
                                                  ((((fc.color & 0xFF0000) * b + (bc.color & 0xFF0000) * (256 - b)) >> 8) & 0xFF0000)); //Red component

                            gui.driver[uGuiDriver.DRIVER_FILL_AREA].driver(x, y, (Int16)(x + actual_char_width - 1), (Int16)(y + font.char_height - 1), color);
                        }
                    }
                }
            }
            else
            {
                /*Not accelerated output*/
                if (font.font_type == uGuiFont.FONT_TYPE.FONT_TYPE_1BPP)
                {
                    index = (uint)((bt - font.start_char) * font.char_height * bn);
                    for (j = 0; j < font.char_height; j++)
                    {
                        xo = (UInt16)x;
                        c = actual_char_width;
                        for (i = 0; i < bn; i++)
                        {
                            //b = font.p[(bt - font.start_char) * (j * bn) + i];
                            b = font.p[index++];
                            for (k = 0; (k < 8) && (c != 0); k++)
                            {
                                if (gui.pset != null)
                                {
                                    if ((b & 0x01) != 0)
                                    {
                                        gui.pset((Int16)xo, (Int16)yo, fc);
                                    }
                                    else
                                    {
                                        gui.pset((Int16)xo, (Int16)yo, bc);
                                    }
                                }
                                b >>= 1;
                                xo++;
                                c--;
                            }
                        }
                        yo++;
                    }
                }
                else if (font.font_type == uGuiFont.FONT_TYPE.FONT_TYPE_8BPP)
                {
                    for (j = 0; j < font.char_height; j++)
                    {
                        xo = (UInt16)x;
                        for (i = 0; i < actual_char_width; i++)
                        {
                            b = font.p[(bt - font.start_char) * (j * bn) + i];
                            color.color = (UInt32)(((((fc.color & 0xFF) * b + (bc.color & 0xFF) * (256 - b)) >> 8) & 0xFF) |//Blue component
                                                  ((((fc.color & 0xFF00) * b + (bc.color & 0xFF00) * (256 - b)) >> 8) & 0xFF00) |//Green component
                                                  ((((fc.color & 0xFF0000) * b + (bc.color & 0xFF0000) * (256 - b)) >> 8) & 0xFF0000)); //Red component
                            if (gui.pset != null)
                            {
                                gui.pset((Int16)xo, (Int16)yo, color);
                            }
                            xo++;
                        }
                        yo++;
                    }
                }
            }
        }

        private static void PutText(uGuiText txt)
        {
            UInt16 wl;
            Int16 xp, yp;
            Int16 xs = txt.a.xs;
            Int16 ys = txt.a.ys;
            Int16 xe = txt.a.xe;
            Int16 ye = txt.a.ye;
            byte align = txt.align;
            Int16 char_width = txt.font.char_width;
            Int16 char_height = txt.font.char_height;
            Int16 char_h_space = txt.h_space;
            Int16 char_v_space = txt.v_space;
            
            if (txt == null) return;
            if (txt.font.p == null) return;
            if (txt.str == null) return;
            if ((ye - ys) < txt.font.char_height) return;

            string[] temp = txt.str.Split(new char[] { '\n', '\r' });
            if (temp == null) return;
            if (temp.Length <= 0) return;

            ArrayList lines = new ArrayList();
            foreach (string s in temp)
            {
                xp = (Int16)(xe - xs + 1);
                wl = 0;
                ushort width = 0;
                string line = string.Empty;
                foreach (char chr in s)
                {
                    if (chr >= txt.font.start_char && chr <= txt.font.end_char)
                    {
                        width = (UInt16)((txt.font.widths != null ? (UInt16)txt.font.widths[chr - txt.font.start_char] : (UInt16)char_width) + char_h_space);
                        wl += width;
                        if (wl >= xp)
                        {
                            lines.Add(line);
                            line = string.Empty;
                            wl = width;
                        }
                        line += chr;
                    }
                }
                if (line.Length > 0)
                {
                    lines.Add(line);
                }
            }

            if (lines == null) return;
            if (lines.Count <= 0) return;

            yp = 0;

            if ((align & (ALIGN_V_CENTER | ALIGN_V_BOTTOM)) != 0)
            {
                yp = (Int16)(ye - ys + 1);
                yp -= (Int16)(char_height * lines.Count);
                yp -= (Int16)(char_v_space * (lines.Count - 1));
                if (yp < 0) yp = 0;
            }

            if ((align & ALIGN_V_CENTER) != 0)
            {
                yp >>= 1;
            }
            yp += ys;

            foreach (string s in lines)
            {
                if (yp + char_height > ye) return;
                wl = 0;
                foreach (char chr in s)
                {
                    if (chr >= txt.font.start_char && chr <= txt.font.end_char)
                    {
                        wl += (UInt16)((txt.font.widths != null ? (UInt16)txt.font.widths[chr - txt.font.start_char] : (UInt16)char_width) + char_h_space);
                    }
                }
                wl -= (UInt16)char_h_space;

                xp = (Int16)(xe - xs + 1);
                xp -= (Int16)wl;
                if (xp < 0) xp = 0;

                if ((align & ALIGN_H_LEFT) != 0) xp = 0;
                else if ((align & ALIGN_H_CENTER) != 0) xp >>= 1;
                xp += xs;

                foreach (char chr in s)
                {
                    if (xp + char_width > xe) break;
                    PutChar(chr, xp, yp, txt.fc, txt.bc, txt.font);
                    xp += (Int16)((txt.font.widths != null ? (Int16)txt.font.widths[chr - txt.font.start_char] : (Int16)char_width) + char_h_space);
                }
                yp += (Int16)(char_height + char_v_space);
            }
        }

        public static void DrawObjectFrame(Int16 xs, Int16 ys, Int16 xe, Int16 ye, uGuiColor[] p)
        {
            int Index = 0;
            // Frame 0
            DrawLine(xs, ys, (Int16)(xe - 1), ys, p[Index++]);
            DrawLine(xs, (Int16)(ys + 1), xs, (Int16)(ye - 1), p[Index++]);
            DrawLine(xs, ye, xe, ye, p[Index++]);
            DrawLine(xe, ys, xe, (Int16)(ye - 1), p[Index++]);
            // Frame 1
            DrawLine((Int16)(xs + 1), (Int16)(ys + 1), (Int16)(xe - 2), (Int16)(ys + 1), p[Index++]);
            DrawLine((Int16)(xs + 1), (Int16)(ys + 2), (Int16)(xs + 1), (Int16)(ye - 2), p[Index++]);
            DrawLine((Int16)(xs + 1), (Int16)(ye - 1), (Int16)(xe - 1), (Int16)(ye - 1), p[Index++]);
            DrawLine((Int16)(xe - 1), (Int16)(ys + 1), (Int16)(xe - 1), (Int16)(ye - 2), p[Index++]);
            // Frame 2
            DrawLine((Int16)(xs + 2), (Int16)(ys + 2), (Int16)(xe - 3), (Int16)(ys + 2), p[Index++]);
            DrawLine((Int16)(xs + 2), (Int16)(ys + 3), (Int16)(xs + 2), (Int16)(ye - 3), p[Index++]);
            DrawLine((Int16)(xs + 2), (Int16)(ye - 2), (Int16)(xe - 2), (Int16)(ye - 2), p[Index++]);
            DrawLine((Int16)(xe - 2), (Int16)(ys + 2), (Int16)(xe - 2), (Int16)(ye - 3), p[Index]);
        }

        public static void DriverRegister(byte type, uGuiDriver.DriverDelegate driver)
        {
            if (type >= uGuiDriver.NUMBER_OF_DRIVERS) return;
            if (gui == null) return;
            if (gui.driver == null) return;

            gui.driver[type] = new uGuiDriver(driver, uGuiDriver.DRIVER_REGISTERED | uGuiDriver.DRIVER_ENABLED);
        }

        public static void DriverEnable(byte type)
        {
            if (type >= uGuiDriver.NUMBER_OF_DRIVERS) return;
            if (gui == null) return;
            if (gui.driver == null) return;
            if (gui.driver[type] == null) return;
            if ((gui.driver[type].state & uGuiDriver.DRIVER_REGISTERED) != 0)
            {
                gui.driver[type].state |= uGuiDriver.DRIVER_ENABLED;
            }
        }

        public static void DriverDisable(byte type)
        {
            if (type >= uGuiDriver.NUMBER_OF_DRIVERS) return;
            if (gui == null) return;
            if (gui.driver == null) return;
            if (gui.driver[type] == null) return;
            if ((gui.driver[type].state & uGuiDriver.DRIVER_REGISTERED) != 0)
            {
                gui.driver[type].state &= (byte)(~uGuiDriver.DRIVER_ENABLED & 0xFF);
            }
        }

        public static void Update()
        {
            uGuiWindow wnd;
            
            if (gui == null) return;

            /* Is somebody waiting for this update? */
            if ((gui.state & UG_SATUS_WAIT_FOR_UPDATE) != 0)
            {
                gui.state &= (byte)(~UG_SATUS_WAIT_FOR_UPDATE & 0xFF);
            }

            /* Keep track of the windows */
            if ( gui.next_window != gui.active_window )
            {
                if ( gui.next_window != null )
                {
                    gui.last_window = gui.active_window;
                    gui.active_window = gui.next_window;

                    /* Do we need to draw an inactive title? */
                    if ((gui.last_window != null) && ((gui.last_window.style & uGuiWindow.WND_STYLE_SHOW_TITLE) != 0) && ((gui.last_window.state & uGuiWindow.WND_STATE_VISIBLE) != 0))
                    {
                        /* Do both windows differ in size */
                        if ( (gui.last_window.xs != gui.active_window.xs) || (gui.last_window.xe != gui.active_window.xe) || (gui.last_window.ys != gui.active_window.ys) || (gui.last_window.ye != gui.active_window.ye) )
                        {
                            /* Redraw title of the last window */
                            gui.last_window.DrawTitle();
                        }
                    }
                    gui.active_window.state &= (byte)(~uGuiWindow.WND_STATE_REDRAW_TITLE & 0xFF);
                    gui.active_window.state |= uGuiWindow.WND_STATE_UPDATE | uGuiWindow.WND_STATE_VISIBLE;
                }
            }

            /* Is there an active window */
            if ( gui.active_window != null )
            {
                wnd = gui.active_window;

                /* Does the window need to be updated? */
                if ((wnd.state & uGuiWindow.WND_STATE_UPDATE) != 0)
                {
                    /* Do it! */
                    wnd.Update();
                }

                /* Is the window visible? */
                if ((wnd.state & uGuiWindow.WND_STATE_VISIBLE) != 0)
                {
                    wnd.ProcessTouchData();
                    wnd.UpdateObjects();
                    wnd.HandleEvents();
                }
            }
        }

        public static void WaitForUpdate()
        {
            if (gui == null) return;
            gui.state |= UG_SATUS_WAIT_FOR_UPDATE;   
            while ((gui.state & UG_SATUS_WAIT_FOR_UPDATE) != 0 )
            {
                Thread.Sleep(10);
            }   
        }

        public static void DrawBMP(Int16 xp, Int16 yp, uGuiBmp bmp)
        {
            Int16 x, y, xs, index;
            byte r, g, b;
            UInt16[] p;
            UInt16 tmp;
            uGuiColor c;

            if (bmp == null) return;
            if (bmp.p == null) return;

            /* Only support 16 BPP so far */
            if (bmp.bpp == uGuiBmp.BMP_BPP_16)
            {
                p = bmp.p;
            }
            else
            {
                return;
            }

            index = 0;
            xs = xp;
            for (y = 0; y < bmp.height; y++)
            {
                xp = xs;
                for (x = 0; x < bmp.width; x++)
                {
                    tmp = p[index++];
                    /* Convert RGB565 to RGB888 */
                    r = (byte)((tmp >> 11) & 0x1F);
                    r <<= 3;
                    g = (byte)((tmp >> 5) & 0x3F);
                    g <<= 2;
                    b = (byte)(tmp & 0x1F);
                    b <<= 3;
                    c = new uGuiColor(((UInt32)r << 16) | ((UInt32)g << 8) | (UInt32)b);
                    DrawPixel(xp++, yp, c);
                }
                yp++;
            }
        }

        public static void TouchUpdate(Int16 xp, Int16 yp, byte state)
        {
            if (gui == null) return;
            gui.touch = new uGuiTouch(xp, yp, state);
        }
    }
}
