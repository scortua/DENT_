using System;

namespace uGUI
{
    public class uGuiCore
    {
        public const byte UG_STATUS_WAIT_FOR_UPDATE = (1<<0);
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
        public In16 x_dim { get; protected set; } = 0;
        public In16 y_dim { get; protected set; } = 0;
        public uGuiTouch touch { get; set; } = null;
        public uGuiWindow next_window { get; set; } = null;
        public uGuiWindow active_window { get; set; } = null;
        public iGuiWindow last_window { get; set; } = null;
        public uGuiConsole console { get; set; } = null;
        public uGuiFont font { get; set; } = null;
        public sbyte char_v_scape { get; set; } = 0;
        public sbyte char_h_scape { get; set; } = 0;
        public uGuiColor fore_color { get; set; } = null;
        public uGuiColor back_color { get; set; } = null;
        public uGuiColor desktop_color { get; set; } = null;
        public byte state { get; set; } = 0;
        public ArrayList driver { get; set; } = null;

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
            driver = new ArrayList();
        }
    }
}
