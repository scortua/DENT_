using System;
using System.Collections;

namespace uGUI
{
    public class uGuiTitle
    {
        public string str { get; set; } = string.Empty;
        public uGuiFont font { get; set; } = null;
        public sbyte h_space { get; set; } = 0;
        public sbyte v_space { get; set; } = 0;
        public byte align { get; set; } = 0;
        public uGuiColor fc { get; set; } = null;
        public uGuiColor bc { get; set; } = null;
        public uGuiColor ifc { get; set; } = null;
        public uGuiColor ibc { get; set; } = null;
        public byte height { get; set; } = 0;

    }
}
