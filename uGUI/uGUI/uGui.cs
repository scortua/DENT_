using System;

namespace uGUI
{
    public class uGUI
    {
        public static uGui gui { get; protected set; } = null;
        private uGuiCore core;
        public uGUI(uGuiCore.PixelSetDelegate pixelSet, Int16 witdh, Int16 height)
        {
            core = new uGuiCore(witdh, height)
            {
                char_h_scape = 1,
                char_v_scape = 1,

            };

            if (pixelSet != null)
            {
                core.PixelSet += pixelSet;
            }

            gui = this;
        }
        public static uGui SelectGui
        {
            set
            {
                if(value != null)
                {
                    gui = value;
                }
            }
        }
    }
}
