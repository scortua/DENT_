using System;

namespace uGUI
{
    public class uGuiDriver
    {
        // driver states
        public const byte DRIVER_REGISTERED = (1 << 0);
        public const byte DRIVER_ENABLED = (1 << 1);
        // supported drivers
        public const byte NUMBER_OF_DRIVERS = 3;
        public const byte DRIVER_DRAW_LINE = 0;
        public const byte DRIVER_FILL_FRAME = 1;
        public const byte DRIVER_FILL_AREA = 2;
        public object driver { get; set; } = null;
        public byte state { get; set; } = 0;
    }
}
