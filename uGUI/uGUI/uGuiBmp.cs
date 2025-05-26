using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiBmp
        {
            public const byte BMP_BPP_1 = (1<<0);
            public const byte BMP_BPP_2 = (1<<1);
            public const byte BMP_BPP_4 = (1<<2);
            public const byte BMP_BPP_8 = (1<<3);
            public const byte BMP_BPP_16 = (1<<4);
            public const byte BMP_BPP_32 = (1<<5);
            public const byte BMP_RGB888 = (1<<0);
            public const byte BMP_RGB565 = (1<<1);
            public const byte BMP_RGB555 = (1<<2);

            /* -------------------------------------------------------------------------------- */
            /* -- BITMAP                                                                     -- */
            /* -------------------------------------------------------------------------------- */
            public UInt16[] p { get; set; }
            public UInt16 width { get; set; }
            public UInt16 height { get; set; }
            public byte bpp { get; set; }
            public byte colors { get; set; }

            public uGuiBmp(UInt16 width = 0, UInt16 height = 0, UInt16[] data = null, byte colors = BMP_RGB565, byte bpp = BMP_BPP_16)
            {
                this.p = data;
                this.width = width;
                this.height = height;
                this.colors = colors;
                this.bpp = bpp;
            }
        }
    }
}
