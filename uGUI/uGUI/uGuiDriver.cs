using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiDriver
        {
            public delegate byte DriverDelegate(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c);

            public const byte DRIVER_REGISTERED = (1 << 0);
            public const byte DRIVER_ENABLED = (1 << 1);

            /* Supported drivers */
            public const byte NUMBER_OF_DRIVERS = 3;
            public const byte DRIVER_DRAW_LINE = 0;
            public const byte DRIVER_FILL_FRAME = 1;
            public const byte DRIVER_FILL_AREA = 2;
            
            /* -------------------------------------------------------------------------------- */
            /* -- µGUI DRIVER                                                                -- */
            /* -------------------------------------------------------------------------------- */

            public DriverDelegate driver { get; set; }
            public byte state { get; set; }

            public uGuiDriver(DriverDelegate driver = null, byte state = 0)
            {
                this.driver = driver;
                this.state = state;
            }
        }
    }
}
