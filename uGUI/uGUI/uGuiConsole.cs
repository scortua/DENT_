using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiConsole
        {
            public Int16 x_pos { get; set; }
            public Int16 y_pos { get; set; }
            public Int16 x_start { get; set; }
            public Int16 y_start { get; set; }
            public Int16 x_end { get; set; }
            public Int16 y_end { get; set; }
            public uGuiColor fore_color { get; set; }
            public uGuiColor back_color { get; set; }

            public uGuiConsole(Int16 x_pos = 0, Int16 y_pos = 0, Int16 x_start = 0, Int16 y_start = 0, Int16 x_end = 0, Int16 y_end = 0, uGuiColor fore_color = null, uGuiColor back_color = null)
            {
                this.x_pos = x_pos;
                this.y_pos = y_pos;
                this.x_start = x_start;
                this.y_start = y_start;
                this.x_end = x_end;
                this.y_end = y_end;
                if (fore_color != null)
                {
                    this.fore_color = fore_color;
                }
                else
                {
                    this.fore_color = new uGuiColor(uGuiColor.C_WHITE);
                }
                if (back_color != null)
                {
                    this.back_color = back_color;
                }
                else
                {
                    this.back_color = new uGuiColor(uGuiColor.C_BLACK);
                }
            }
        }
    }
}
