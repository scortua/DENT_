using System;

namespace uGUI
{
    public class uGui_Area
    {
        public Int16 xs { get; set; } = 0;
        public Int16 ys { get; set; } = 0;
        public Int16 xe { get; set; } = 0;
        public Int16 ye { get; set; } = 0;
        public uGuiArea(Int16 xs=0, Int16 ys=0, Int16 xe=0, Int16 ye=0)
        {
            this.xs = xs;
            this.ys = ys;   
            this.xe = xe;
            this.ye = ye;
        }
    }
}
