using System;

namespace uGUI
{
    internal class uGuiText
    {
        public string str { get; set; } = string.Empty;
        public uGuiFont font { get; set; } = null;
        public uGuiArea area { get; set; } = null;
        public uGuiColor fc { get; set; } = null;
        public uGuiColor bc { get; set; } = null;
        public byte align { get; set; } = 0;
        public Int16 h_space { get; set; } = 0;
        public Int16 v_space { get; set; } = 0;
    }
}
