using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Device.Gpio;
using nanoFramework.Hardware.Esp32;
using nanoFramework.UI;
using nanoFramework.UI.GraphicDrivers;
using uGUI;

namespace ESP32_DisplayTest
{
    public class Program
    {
        private static uGUI.uGUI Graphics; 

        private const int ChipSelect = 5;
        private const int DataCommand = 4;
        private const int Reset = 8;
        private const int backlight = 15;
        public static void Main()
        {
            Debug.WriteLine("Hello from and generic grahic drivers!");
            var gpioController = new GpioController();

            GpioPin Bl = gpioController.OpenPin(backlight, PinMode.Output);
            Bl.Write(PinValue.Low);

            // You **MUST** have a build of nanoFramework with a generic graphic driver

            // If you're using an ESP32, don't forget to set the pins for the screen
            // Set the pins for the screen
            Configuration.SetPinFunction(7, DeviceFunction.SPI1_MOSI);  // LCD SDA
            Configuration.SetPinFunction(6, DeviceFunction.SPI1_CLOCK); // LCD SCL 
            // This is not used but must be defined
            Configuration.SetPinFunction(46, DeviceFunction.SPI1_MISO);  // LCD MISO SIN importancia

            var displaySpiConfig = new SpiConfiguration(
                1,
                ChipSelect,
                DataCommand,
                Reset,
                -1); // blacklight);

            // Get the generic driver
            GraphicDriver graphicDriver = St7789.GraphicDriver;

            // As optional, you can adjust anything
            graphicDriver.OrientationPortrait = new byte[]
            {
                (byte)GraphicDriverCommandType.Command, 2, 0x36, 0x88,
            };

            var screenConfig = new ScreenConfiguration(
                0,
                20, // offset inicia con 320
                240, // vertical resolution
                280, // horizontal resolution
                graphicDriver);

            var init = DisplayControl.Initialize(
                displaySpiConfig,
                screenConfig);
                //,1024);

            Bl.Write(PinValue.High);
            Debug.WriteLine($"Screen initialized");

            Graphics = new uGUI.uGUI(DisplaySetPixel, 240, 280);

            Graphics.FillRoundFrame(20,20,220,260,20,new uGuiColor(uGuiColor.C_GREEN));

            /*ushort[] toSend = new ushort[100];
            var blue = Color.Blue.ToBgr565();
            var red = Color.Red.ToBgr565();
            var green = Color.Green.ToBgr565();
            var white = Color.White.ToBgr565();

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = blue;
            }

            DisplayControl.Write(10, 10, 10, 10, toSend); // x, y, width, height, data

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = red;
            }

            DisplayControl.Write(220, 10, 10, 10, toSend); // x, y, width, height, data

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = green;
            }

            DisplayControl.Write(10, 260, 10, 10, toSend); // x, y, width, height, data

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = white;
            }

            DisplayControl.Write(220, 260, 10, 10, toSend); // x, y, width, height, data*/

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
        private static void DisplaySetPixel(Int16 x, Int16 y, uGuiColor c)
        {
            DisplayControl.WritePoint((ushort)x, (ushort)y, c.Color);
        }
    }
}
