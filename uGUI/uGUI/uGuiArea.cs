using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiArea
        {
            public Int16 xs { get; set; }
            public Int16 ys { get; set; }
            public Int16 xe { get; set; }
            public Int16 ye { get; set; }

            public uGuiArea(Int16 xs = -1, Int16 ys = -1, Int16 xe = -1, Int16 ye = -1)
            {
                this.xs = xs;
                this.ys = ys;
                this.xe = xe;
                this.ye = ye;
            }
        }
    }
}
