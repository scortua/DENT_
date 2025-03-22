using System;

namespace uGUI
{
    public class uGuiImage
    {
        // imagines type
        public const byte IMG_TYPE_BMP = (1 << 0);
        public byte[] img { get; set; } = null;
        public byte type { get; set; } = 0;

    }
}
