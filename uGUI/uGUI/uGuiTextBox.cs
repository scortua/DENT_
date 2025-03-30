using System;
using System.Collections;

namespace uGUI
{
    public class uGuiTextBox
    {
        public byte state { get; set; } = 0;
        public byte style { get; set; } = 0;
        public uGuiColor fc { get; set; } = null;
        public uGuiColor bc { get; set; } = null;
        public uGuiFont font { get; set; } = null;
        public byte align { get; set; } = 0;
        public sbyte h_space { get; set; } = 0;
        public sbyte v_space { get; set; } = 0;
        public string str { get; set; } = null;
    }
}
