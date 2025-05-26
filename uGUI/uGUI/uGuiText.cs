using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiText
        {
            public string str { get; set; }
            public uGuiFont font { get; set; }
            public uGuiArea a { get; set; }
            public uGuiColor fc { get; set; }
            public uGuiColor bc { get; set; }
            public byte align { get; set; }
            public Int16 h_space { get; set; }
            public Int16 v_space { get; set; }
        }
    }
}
