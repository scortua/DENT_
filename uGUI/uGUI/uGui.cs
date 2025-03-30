using System;
using System.Collections;

namespace uGUI
{
    public class uGUI : uGuiCore
    {
        //public static uGui gui { get; protected set; } = null;
        //private static uGui gui = null;
        public uGUI(uGuiCore.PixelSetDelegate pixelSet, Int16 witdh, Int16 height) : base(witdh, height)
        {
            char_h_scape = 1;
            char_v_scape = 1;
            if (pixelSet != null)
            {
                PixelSet += pixelSet;
            }
            /*core = new uGuiCore(witdh, height)
            {
                char_h_scape = 1,
                char_v_scape = 1,

            };*/

            if (pixelSet != null)
            {
                PixelSet += pixelSet;
            }

            //gui = this;
        }
        /*
        public static uGui SelectGui
        {
            set
            {
                if(value != null)
                {
                    gui = value;
                }
            }
        }*/
        public uGuiFont FontSelect 
        {
            set
            {
                if (value != null)
                {
                    gui.core.font = value;
                }
            }
        } 
    }
}
