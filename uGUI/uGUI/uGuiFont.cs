using System;

namespace uGUI
{
    public class uGuiFont
    {
        public enum FontType
        {
            FONT_TYPE_1BPP = 0,
            FONT_TYPE_8BPP
        }
        public byte[] p { get; set; } = null;
        public FontType type { get; set; } = FontType.FONT_TYPE_1BPP;
        public Int16 char_width { get; set; } = 0;
        public Int16 char_height { get; set; } = 0;
        public UInt16 start_char { get; set; } = 0;
        public UInt16 end_char { get; set; } = 0;
        public byte[] widths { get; set; } = null;

    }
}
