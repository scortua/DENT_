using System;
using System.Collections;

namespace uGUI
{
    public class uGuiCore
    {
        public const byte UG_STATUS_WAIT_FOR_UPDATE = (1 << 0);
        public delegate void PixelSetDelegate(Int16 x, Int16 y, uGuiColor color);
        public class uGuiConsole
        {
            public Int16 x_pos { get; set; } = 0;
            public Int16 y_pos { get; set; } = 0;
            public Int16 x_start { get; set; } = 0;
            public Int16 y_start { get; set; } = 0;
            public Int16 x_end { get; set; } = 0;
            public Int16 y_end { get; set; } = 0;
            public uGuiColor fore_color { get; set; } = null;
            public uGuiColor back_color { get; set; } = null;
        }
        public event PixelSetDelegate PixelSet = null;
        public Int16 x_dim { get; protected set; } = 0;
        public Int16 y_dim { get; protected set; } = 0;
        public uGuiTouch touch { get; set; } = null;
        public uGuiWindow next_window { get; set; } = null;
        public uGuiWindow active_window { get; set; } = null;
        public uGuiWindow last_window { get; set; } = null;
        public uGuiConsole console { get; set; } = null;
        public uGuiFont font { get; set; } = null;
        public sbyte char_v_scape { get; set; } = 0;
        public sbyte char_h_scape { get; set; } = 0;
        public uGuiColor fore_color { get; set; } = null;
        public uGuiColor back_color { get; set; } = null;
        public uGuiColor desktop_color { get; set; } = null;
        public byte state { get; set; } = 0;
        public uGuiDriver[] driver { get; set; } = null;

        public uGuiCore(Int16 witdh, Int16 height)
        {
            if (witdh <= 0 || height <= 0)
            {
                throw new ArgumentException("display Width or height out of range");
            }
            x_dim = witdh;
            y_dim = height;
            console = new uGuiConsole()
            {
                x_start = 4,
                y_start = 4,
                x_end = (Int16)(x_dim - 5),
                y_end = (Int16)(y_dim - 5),
                x_pos = (Int16)(x_dim - 5),
                y_pos = (Int16)(y_dim - 5),
            };
            font = new uGuiFont();
#if USE_COLOR_RGB888
            desktop_color = new uGuiColor() { Color = 5E8BEf };
#else
            desktop_color = new uGuiColor() { Color = 0x5C5D };
#endif
            fore_color = new uGuiColor(uGuiColor.C_WHITE);
            back_color = new uGuiColor(uGuiColor.C_BLACK);
            next_window = null;
            active_window = null;
            last_window = null;
            driver = new uGuiDriver[uGuiDriver.NUMBER_OF_DRIVERS];

        }
        public void FillScreen(uGuiColor color)
        {
            FillFrame(0, 0, x_dim, y_dim, color);
        }
        public void FillFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor color)
        {
            Int16 n, m;

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
            if (driver != null)
            {
                if ((driver.length >= uGuiDriver.DRIVER_FILL_FRAME + 1) && (driver[uGuiDriver.DRIVER_FILL_FRAME] != null))
                {
                    if (driver[uGuiDriver.DRIVER_FILL_FRAME].state && (uGuiDriver.DRIVER_ENABLED != 0))
                    {

                    }
                }
            }
            if (PixelSet != null)
            {
                for (m = y1; m <= y2; m++)
                {
                    for (n = x1; n <= x2; n++)
                    {
                        PixelSet(n, m, color);
                    }
                }
            }
        }
        public void DrawMesh(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            Int16 n, m;

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
                    PixelSet(n, m, c);
                }
            }
        }
        public void FillRoundFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, Int16 r, uGuiColor c)
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
            if (r <= 0)
            {
                return;
            }

            xd = (Int16)(3 - (r << 1));
            x = 0;
            y = r;

            FillFrame((Int16)(x1 + r), y1, (Int16)(x2 - r), y2, c);

            while (x <= y)
            {
                if (y > 0)
                {
                    DrawLine((Int16)(x2 + x - r), (Int16)(y1 - y + r), (Int16)(x2 + x - r), (Int16)(y + y2 - r, c));
                    DrawLine((Int16)(x1 - x + r), (Int16)(y1 - y + r), (Int16)(x1 - x + r), (Int16)(y + y2 - r, c));
                }
                if (x > 0)
                {
                    DrawLine((Int16)(x1 - y + r), (Int16)(y1 - x + r), (Int16)(x1 - y + r), (Int16)(x + y2 - r, c));
                    DrawLine((Int16)(x2 + y - r), (Int16)(y1 - x + r), (Int16)(x2 + y - r), (Int16)(x + y2 - r, c));
                }
                if (xd < 0)
                {
                    xd += (x << 2) + 6;
                }
                else
                {
                    xd += ((x - y) << 2) + 10;
                    y--;
                }
                x++;
            }
        }
        public void DrawLine(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            Int16 n, dx, dy, sgndx, sgndy, dxabs, dyabs, x, y, drawx, drawy;

            /* Is hardware acceleration available? */
            if (driver != null)
            {
                if ((driver.length >= uGuiDriver.DRIVER_FILL_FRAME + 1) && (driver[uGuiDriver.DRIVER_FILL_FRAME] != null))
                {
                    if (driver[uGuiDriver.DRIVER_FILL_FRAME].state && (uGuiDriver.DRIVER_ENABLED != 0))
                    {

                    }
                }
            }

            dx = (Int16)(x2 - x1);
            dy = (Int16)(y2 - y1);
            dxabs = (dx > 0) ? (Int16)dx : (Int16)(- dx);
            dyabs = (dy > 0) ? (Int16)dy : (Int16)(-dy);
            sgndx = (dx > 0) ? 1 : (Int16)(- 1);
            sgndy = (dy > 0) ? 1 : (Int16)(- 1);
            x = (Int16)(dyabs >> 1);
            y = (Int16)(dxabs >> 1);
            drawx = x1;
            drawy = y1;

            if (PixelSet != null)
            {
                PixelSet(drawx, drawy, c);

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
                        PixelSet(drawx, drawy, c);
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
                        PixelSet(drawx, drawy, c);
                    }
                }
            }
        }
        public void UG_DrawFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            DrawLine(x1, y1, x2, y1, c);
            DrawLine(x1, y2, x2, y2, c);
            DrawLine(x1, y1, x1, y2, c);
            DrawLine(x2, y1, x2, y2, c);
        }
    }
}
