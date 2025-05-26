using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using nanoFramework.Hardware.Esp32;
using nanoFramework.UI;
using nanoFramework.UI.GraphicDrivers;
using System.Device.Gpio;
using uGUI;
using ESP32_DisplayTest.Helpers;
using static uGUI.uGui;

namespace ESP32_DisplayTest
{
    public class Program
    {
        private static uGui gui; 

        private const int ChipSelect = 5;
        private const int DataCommand = 4;
        private const int Reset = 8;
        private const int BackLight = 15;
        private const int TpReset = 13;
        private const int TpInt = 14;

        private static CST816 TouchPanel = null;

        private static object SyncGraphics = new object();
        // Status Window Objects
        private static uGui.uGuiWindow StatusWnd;
        private static uGui.uGuiButton btnMenu;
        private static uGui.uGuiCheckBox NetworkConnected;
        private static uGui.uGuiCheckBox BrokerConnected;
        private static uGui.uGuiTextBox IpAddr;
        private static string StatusWndTitle;
        private static bool DisplayUpdate = false;
        // Menu Window Objects
        /*private uGui.uGuiWindow MenuWnd;
        private uGui.uGuiListBox lbxMenu;
        private uGui.uGuiButton btnBack1;
        private uGui.uGuiButton btnNext1;
        private uGui.uGuiButton btnSelect1;*/
        //Confirm Reboot Window Objets

        public static void Main()
        {
            Debug.WriteLine("Hello from and generic grahic drivers!");
            var gpioController = new GpioController();

            GpioPin Bl = gpioController.OpenPin(BackLight, PinMode.Output);
            Bl.Write(PinValue.Low);

            GpioPin TpRst = gpioController.OpenPin(TpReset, PinMode.Output);
            TpRst.Write(PinValue.High);

            TouchPanel = new CST816(10, 11, TpRst);
            TouchPanel.Init(CST816.TouchPadMode.MODE_TOUCH);
            // You **MUST** have a build of nanoFramework with a generic graphic driver

            // If you're using an ESP32, don't forget to set the pins for the screen
            // Set the pins for the screen
            Configuration.SetPinFunction(7, DeviceFunction.SPI1_MOSI);
            Configuration.SetPinFunction(6, DeviceFunction.SPI1_CLOCK);
            // This is not used but must be defined
            Configuration.SetPinFunction(46, DeviceFunction.SPI1_MISO);

            var displaySpiConfig = new SpiConfiguration(
                1,
                ChipSelect,
                DataCommand,
                Reset,
                -1);// BackLight);

            // Get the generic driver
            GraphicDriver graphicDriver = St7789.GraphicDriver;

            // As optional, you can adjust anything
            graphicDriver.OrientationPortrait = new byte[]
            {
                (byte)GraphicDriverCommandType.Command, 2, 0x36, 0x88,
            };

            Diagnostics.PrintMemory("Main4");

            var screenConfig = new ScreenConfiguration(
                0,
                20,
                240,
                280,
                graphicDriver);

            var init = DisplayControl.Initialize(
                displaySpiConfig,
                screenConfig);//,1024);

            Bl.Write(PinValue.High);
            Debug.WriteLine($"Screen initialized");

            uGui.InitColorPalete();
            gui = new uGui(new uGui.PixelSetDelegate(DisplaySetPixet), (Int16)240, (Int16)280);
            uGui.SelectGUI = gui;
            gui.FontSelect = uGui.uGuiFont.FONT_12X20;
            uGui.DriverRegister(uGuiDriver.DRIVER_DRAW_LINE, DisplayDriverDrawLine);
            uGui.DriverRegister(uGuiDriver.DRIVER_FILL_FRAME, DisplayDriverFillFrame);
            uGui.DriverRegister(uGuiDriver.DRIVER_FILL_AREA, DisplayDriverFillFrame);
            uGui.DriverEnable(uGuiDriver.DRIVER_DRAW_LINE);
            uGui.DriverEnable(uGuiDriver.DRIVER_FILL_FRAME);
            uGui.DriverEnable(uGuiDriver.DRIVER_FILL_AREA);
            /*uGuiCore.CoreInit(240, 280);
            uGuiCore.PixelSet += DisplaySetPixet;
            uGuiCore.SetDriver(uGuiDriver.DRIVER_DRAW_LINE, DisplayDriverDrawLine);
            uGuiCore.SetDriver(uGuiDriver.DRIVER_FILL_FRAME, DisplayDriverFillFrame);
            uGuiCore.EnableDriver(uGuiDriver.DRIVER_DRAW_LINE, true);
            uGuiCore.EnableDriver(uGuiDriver.DRIVER_FILL_FRAME, true);*/


            /*DateTime Start = DateTime.UtcNow;
            uGui.FillRoundFrame(50, 50, 200, 200, 20, new uGuiColor(uGuiColor.C_GREEN));
            TimeSpan Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Display Flush: " + Diff.TotalMilliseconds.ToString("F2"));


            Start = DateTime.UtcNow;
            DisplayFlush();
            Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Display Flush: " + Diff.TotalMilliseconds.ToString("F2"));*/

            //uGui.PutString(10, 160, "HOLA MUNDO!");

            DateTime Start = DateTime.UtcNow;
            InitGraphicObjects();
            TimeSpan Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Init Graphic Objects: " + Diff.TotalMilliseconds.ToString("F2"));

            Start = DateTime.UtcNow;
            DisplayFlush();
            Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Display Flush: " + Diff.TotalMilliseconds.ToString("F2"));

            /*ushort[] toSend = new ushort[100];
            var blue = Color.Blue.ToBgr565();
            var red = Color.Red.ToBgr565();
            var green = Color.Green.ToBgr565();
            var white = Color.White.ToBgr565();

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = blue;
            }

            DisplayControl.Write(10, 10, 10, 10, toSend);

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = red;
            }

            DisplayControl.Write(220, 10, 10, 10, toSend);

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = green;
            }

            DisplayControl.Write(10, 260, 10, 10, toSend);

            for (int i = 0; i < toSend.Length; i++)
            {
                toSend[i] = white;
            }

            DisplayControl.Write(220, 260, 10, 10, toSend);
            Diagnostics.PrintMemory("Main 6");*/

            DateTime Last = DateTime.UtcNow;
            while (true)
            {
                Thread.Sleep(100);
                if(DateTime.UtcNow.Minute != Last.Minute)
                {
                    StatusWndTitle = DateTime.UtcNow.ToString("yy/MM/dd HH:mm");
                    StatusWnd.SetTitleText(StatusWndTitle);
                    Last = DateTime.UtcNow;
                }
                switch(TouchPanel.Read())
                {
                    case CST816.TouchState.TS_PRESSED:
                        Debug.WriteLine("Pressed X: " + TouchPanel.X + ", Y: " + TouchPanel.Y);
                        uGui.TouchUpdate((short)TouchPanel.X, (short)TouchPanel.Y, uGuiTouch.TOUCH_STATE_PRESSED);
                        break;
                    case CST816.TouchState.TS_RELEASED:
                        Debug.WriteLine("Released X: " + TouchPanel.X + ", Y: " + TouchPanel.Y);
                        uGui.TouchUpdate((short)TouchPanel.X, (short)TouchPanel.Y, uGuiTouch.TOUCH_STATE_RELEASED);
                        break;
                    default:
                        break;
                }
                uGui.Update();
                DisplayFlush();
            }
        }

        private static void DisplaySetPixet(Int16 x, Int16 y, uGuiColor c)
        {
            //DisplayControl.WritePoint((ushort)x, (ushort)y, c.Color);
            DisplayControl.FullScreen.SetPixel(x, y, Color.FromArgb(c.R, c.G, c.B));
            DisplayUpdate = true;
        }

        private static void DisplayFlush()
        {
            //DisplayControl.Write(0,0,(ushort)Graphics.x_dim, (ushort)Graphics.y_dim, Graphics.MemBuffer);
            if (DisplayUpdate)
            {
                DisplayControl.FullScreen.Flush();
                DisplayUpdate = false;
            }
        }

        private static byte DisplayDriverDrawLine(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            DateTime Start = DateTime.UtcNow;
            //DisplayControl.FullScreen.DrawLine(Color.FromArgb(c.R, c.G, c.B), 1, x1, y1, x2, y2);
            DisplayControl.FullScreen.DrawLine(Color.FromArgb((int)c.color), 1, x1, y1, x2, y2);
            TimeSpan Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Display Driver Draw Line: " + Diff.TotalMilliseconds.ToString("F2"));
            DisplayUpdate = true;
            return (byte)UG_RESULT_OK;
        }
        /       private static byte DisplayDriverFillFrame(Int16 x1, Int16 y1, Int16 x2, Int16 y2, uGuiColor c)
        {
            //DisplayControl.FullScreen.FillRectangle(x1, y1, (x2 - x1) + 1, (y2 - y1) + 1, Color.FromArgb(c.R, c.G, c.B));
            DateTime Start = DateTime.UtcNow;
            DisplayControl.FullScreen.FillRectangle(x1, y1, (x2 - x1) + 1, (y2 - y1) + 1, Color.FromArgb((int)c.color));//c.R, c.G, c.B));
            TimeSpan Diff = DateTime.UtcNow - Start;
            Debug.WriteLine("Display Driver Fill Frame: " + Diff.TotalMilliseconds.ToString("F2"));
            DisplayUpdate = true;
            return (byte)UG_RESULT_OK;
        }


        private static void InitGraphicObjects()
        {
            Debug.WriteLine("Init Graphic Objects");
            lock (SyncGraphics)
            {
                DateTime Start = DateTime.UtcNow;
                #region Status Window

                try
                {
                    StatusWnd = new uGui.uGuiWindow(StatusWindowMessage);
                    StatusWnd.SetTitleText("STATUS");
                    StatusWnd.SetTitleTextAlignment(ALIGN_CENTER);
                    StatusWnd.SetTitleHeight(40);
                    //StatusWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    //StatusWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //StatusWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //StatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    //StatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnMenu = new uGui.uGuiButton(StatusWnd, uGui.uGuiButton.BTN_ID_0, 40, 170, 200, 230);
                    btnMenu.SetFont(uGui.uGuiFont.FONT_12X20);
                    btnMenu.SetText("MENU");
                    btnMenu.SetStyle((byte)(btnMenu.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    //btnMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    //btnMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //btnMenu.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //btnMenu.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    NetworkConnected = new uGui.uGuiCheckBox(StatusWnd, uGui.uGuiCheckBox.CHB_ID_0, 10, 10, 230, 50);
                    NetworkConnected.SetFont(uGui.uGuiFont.FONT_12X20);
                    NetworkConnected.SetText("Network Connected");
                    //NetworkConnected.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //NetworkConnected.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    BrokerConnected = new uGui.uGuiCheckBox(StatusWnd, uGui.uGuiCheckBox.CHB_ID_1, 10, 60, 230, 100);
                    BrokerConnected.SetFont(uGui.uGuiFont.FONT_12X20);
                    BrokerConnected.SetText("Broker Connected");
                    //BrokerConnected.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //BrokerConnected.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    IpAddr = new uGui.uGuiTextBox(StatusWnd, uGui.uGuiTextBox.TXB_ID_0, 10, 110, 230, 150);
                    IpAddr.SetFont(uGui.uGuiFont.FONT_12X20);
                    IpAddr.SetAlignment(uGui.ALIGN_CENTER);
                    IpAddr.SetText("IP: 0.0.0.0");
                    //IpAddr.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    //IpAddr.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("InitGraphicObjects Status Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                TimeSpan Diff = DateTime.UtcNow - Start;
                Debug.WriteLine("Init Status Window: " + Diff.TotalMilliseconds.ToString("F2"));
                /*#region Menu Window
                try
                {
                    MenuWnd = new uGui.uGuiWindow(MenuWindowMessage);
                    MenuWnd.SetTitleText("MENU");
                    MenuWnd.SetTitleHeight(10);
                    MenuWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    MenuWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    MenuWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    MenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    MenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack1 = new uGui.uGuiButton(MenuWnd, uGui.uGuiButton.BTN_ID_1, 0, 42, 43, 52);
                    btnBack1.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack1.SetText("BACK");
                    btnBack1.SetStyle((byte)(btnBack1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack1.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack1.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack1.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack1.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext1 = new uGui.uGuiButton(MenuWnd, uGui.uGuiButton.BTN_ID_2, 45, 42, 86, 52);
                    btnNext1.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext1.SetText("NEXT");
                    btnNext1.SetStyle((byte)(btnNext1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext1.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext1.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext1.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext1.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect1 = new uGui.uGuiButton(MenuWnd, uGui.uGuiButton.BTN_ID_3, 88, 42, 127, 52);
                    btnSelect1.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect1.SetText("SELECT");
                    btnSelect1.SetStyle((byte)(btnSelect1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect1.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect1.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect1.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect1.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxMenu = new uGui.uGuiListBox(MenuWnd, uGui.uGuiListBox.LSB_ID_0, 2, 2, 125, 40);
                    lbxMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxMenu.AddItem("Signals");
                    lbxMenu.AddItem("Alerts");
                    lbxMenu.AddItem("Messages");
                    lbxMenu.AddItem("Config");
                    lbxMenu.AddItem("Reboot");
                    lbxMenu.AddItem("Power Off");
                    lbxMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    lbxMenu.SetIndex(0);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion
                
                #region Confirm Reboot Window

                try
                {
                    RebootWnd = new uGui.uGuiWindow(ConfirmRebootWindowMessage);
                    RebootWnd.SetTitleText("CONFIRM");
                    RebootWnd.SetTitleHeight(10);
                    RebootWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    RebootWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    RebootWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    RebootWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    RebootWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes1 = new uGui.uGuiButton(RebootWnd, uGui.uGuiButton.BTN_ID_4, 0, 42, 43, 52);
                    btnYes1.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnYes1.SetText("YES");
                    btnYes1.SetStyle((byte)(btnYes1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnYes1.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes1.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes1.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes1.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo1 = new uGui.uGuiButton(RebootWnd, uGui.uGuiButton.BTN_ID_5, 88, 42, 127, 52);
                    btnNo1.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNo1.SetText("NO");
                    btnNo1.SetStyle((byte)(btnNo1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNo1.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNo1.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo1.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo1.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtReboot = new uGui.uGuiTextBox(RebootWnd, uGui.uGuiTextBox.TXB_ID_1, 1, 1, 126, 39);
                    txtReboot.SetFont(uGui.uGuiFont.FONT_8X12);
                    txtReboot.SetText("REBOOT SYSTEM?");
                    txtReboot.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtReboot.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    //txtReboot.SetStyle((byte)(txtReboot.GetStyle() | uGui.uGuiTextBox.TXB_STYLE_NO_BORDERS));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Confirm Power Off Window

                try
                {
                    PowerOffWnd = new uGui.uGuiWindow(ConfirmPowerOffWindowMessage);
                    PowerOffWnd.SetTitleText("CONFIRM");
                    PowerOffWnd.SetTitleHeight(10);
                    PowerOffWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    PowerOffWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    PowerOffWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    PowerOffWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    PowerOffWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes2 = new uGui.uGuiButton(PowerOffWnd, uGui.uGuiButton.BTN_ID_6, 0, 42, 43, 52);
                    btnYes2.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnYes2.SetText("YES");
                    btnYes2.SetStyle((byte)(btnYes2.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnYes2.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes2.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes2.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes2.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo2 = new uGui.uGuiButton(PowerOffWnd, uGui.uGuiButton.BTN_ID_7, 88, 42, 127, 52);
                    btnNo2.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNo2.SetText("NO");
                    btnNo2.SetStyle((byte)(btnNo2.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNo2.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNo2.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo2.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo2.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtPowerOff = new uGui.uGuiTextBox(PowerOffWnd, uGui.uGuiTextBox.TXB_ID_2, 1, 1, 126, 39);
                    txtPowerOff.SetFont(uGui.uGuiFont.FONT_8X12);
                    txtPowerOff.SetText("POWER OFF\nSYSTEM?");
                    txtPowerOff.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtPowerOff.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    //txtReboot.SetStyle((byte)(txtReboot.GetStyle() | uGui.uGuiTextBox.TXB_STYLE_NO_BORDERS));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Login Window
                try
                {
                    LoginWnd = new uGui.uGuiWindow(LoginWindowMessage);
                    LoginWnd.SetTitleText("LOGIN");
                    LoginWnd.SetTitleHeight(10);
                    LoginWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    LoginWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    LoginWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    LoginWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    LoginWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack2 = new uGui.uGuiButton(LoginWnd, uGui.uGuiButton.BTN_ID_8, 0, 42, 43, 52);
                    btnBack2.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack2.SetText("BACK");
                    btnBack2.SetStyle((byte)(btnBack2.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack2.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack2.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack2.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack2.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext2 = new uGui.uGuiButton(LoginWnd, uGui.uGuiButton.BTN_ID_9, 45, 42, 86, 52);
                    btnNext2.SetFont(uGui.uGuiFont.FONT_6X8);
                    btnNext2.SetText(">");
                    btnNext2.SetStyle((byte)(btnNext2.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext2.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext2.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext2.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext2.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet2 = new uGui.uGuiButton(LoginWnd, uGui.uGuiButton.BTN_ID_10, 88, 42, 127, 52);
                    btnSet2.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSet2.SetText("SET");
                    btnSet2.SetStyle((byte)(btnSet2.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSet2.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSet2.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet2.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet2.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtNum = new uGui.uGuiTextBox[4];
                    Numbers = new string[txtNum.Length];
                    LoginNumIndex = 0;
                    LogintxtNumber = 0;
                    for (int i = 0; i < txtNum.Length; i++)
                    {
                        Numbers[i] = "0";
                        txtNum[i] = new uGui.uGuiTextBox(LoginWnd, (byte)(uGui.uGuiTextBox.TXB_ID_3 + i), (short)(9 + (30 * i)), 10, (short)(29 + (30 * i)), 30);
                        txtNum[i].SetFont(uGui.uGuiFont.FONT_12X16);
                        txtNum[i].SetText("0");
                        txtNum[i].SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                        txtNum[i].SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    }
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Config Menu Window
                try
                {
                    ConfigMenuWnd = new uGui.uGuiWindow(ConfigMenuWindowMessage);
                    ConfigMenuWnd.SetTitleText("CONFIG MENU");
                    ConfigMenuWnd.SetTitleHeight(10);
                    ConfigMenuWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    ConfigMenuWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    ConfigMenuWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    ConfigMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    ConfigMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack3 = new uGui.uGuiButton(ConfigMenuWnd, uGui.uGuiButton.BTN_ID_11, 0, 42, 43, 52);
                    btnBack3.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack3.SetText("BACK");
                    btnBack3.SetStyle((byte)(btnBack3.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack3.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack3.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack3.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack3.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext3 = new uGui.uGuiButton(ConfigMenuWnd, uGui.uGuiButton.BTN_ID_12, 45, 42, 86, 52);
                    btnNext3.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext3.SetText("NEXT");
                    btnNext3.SetStyle((byte)(btnNext3.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext3.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext3.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext3.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext3.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect3 = new uGui.uGuiButton(ConfigMenuWnd, uGui.uGuiButton.BTN_ID_13, 88, 42, 127, 52);
                    btnSelect3.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect3.SetText("SELECT");
                    btnSelect3.SetStyle((byte)(btnSelect3.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect3.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect3.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect3.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect3.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxConfigMenu = new uGui.uGuiListBox(ConfigMenuWnd, uGui.uGuiListBox.LSB_ID_1, 2, 2, 125, 40);
                    lbxConfigMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxConfigMenu.AddItem("Client Id");
                    lbxConfigMenu.AddItem("Service Name");
                    lbxConfigMenu.AddItem("Device Id");
                    lbxConfigMenu.AddItem("RTU Baudrate");
                    lbxConfigMenu.AddItem("RTU Parity");
                    lbxConfigMenu.AddItem("RTU Stop Bits");
                    lbxConfigMenu.AddItem("Broker Address");
                    lbxConfigMenu.AddItem("Broker Port");
                    lbxConfigMenu.AddItem("Broker SSL Port");
                    lbxConfigMenu.AddItem("Broker User");
                    //lbxConfigMenu.AddItem("Broker Password");
                    lbxConfigMenu.AddItem("Use SSL");
                    lbxConfigMenu.AddItem("Report Time");
                    lbxConfigMenu.AddItem("Visualization Time");
                    lbxConfigMenu.AddItem("Log Enable");
                    lbxConfigMenu.AddItem("Log Level");
                    lbxConfigMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxConfigMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    lbxConfigMenu.SetIndex(0);
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Info Field Window

                try
                {
                    FieldInfoWnd = new uGui.uGuiWindow(InfoFieldWindowMessage);
                    FieldInfoWnd.SetTitleText("");
                    FieldInfoWnd.SetTitleHeight(10);
                    FieldInfoWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    FieldInfoWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    FieldInfoWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    FieldInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    FieldInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack4 = new uGui.uGuiButton(FieldInfoWnd, uGui.uGuiButton.BTN_ID_14, 0, 42, 43, 52);
                    btnBack4.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack4.SetText("BACK");
                    btnBack4.SetStyle((byte)(btnBack4.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack4.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack4.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack4.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack4.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtField = new uGui.uGuiTextBox(FieldInfoWnd, uGui.uGuiTextBox.TXB_ID_7, 1, 1, 126, 39);
                    txtField.SetFont(uGui.uGuiFont.FONT_6X8);
                    txtField.SetText("");
                    txtField.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtField.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Info Field Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signals List Window
                try
                {
                    SignalsWnd = new uGui.uGuiWindow(SignalsWindowMessage);
                    SignalsWnd.SetTitleText("SIGNALS LIST");
                    SignalsWnd.SetTitleHeight(10);
                    SignalsWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalsWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalsWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack5 = new uGui.uGuiButton(SignalsWnd, uGui.uGuiButton.BTN_ID_15, 0, 42, 43, 52);
                    btnBack5.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack5.SetText("BACK");
                    btnBack5.SetStyle((byte)(btnBack5.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack5.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack5.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack5.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack5.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext5 = new uGui.uGuiButton(SignalsWnd, uGui.uGuiButton.BTN_ID_16, 45, 42, 86, 52);
                    btnNext5.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext5.SetText("NEXT");
                    btnNext5.SetStyle((byte)(btnNext5.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext5.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext5.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext5.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext5.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect5 = new uGui.uGuiButton(SignalsWnd, uGui.uGuiButton.BTN_ID_17, 88, 42, 127, 52);
                    btnSelect5.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect5.SetText("SELECT");
                    btnSelect5.SetStyle((byte)(btnSelect5.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect5.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect5.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect5.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect5.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignals = new uGui.uGuiListBox(SignalsWnd, uGui.uGuiListBox.LSB_ID_2, 2, 2, 125, 40);
                    lbxSignals.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignals.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignals.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Signals List Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Menu Window
                try
                {
                    SignalMenuWnd = new uGui.uGuiWindow(SignalMenuWindowMessage);
                    SignalMenuWnd.SetTitleText("");
                    SignalMenuWnd.SetTitleHeight(10);
                    SignalMenuWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalMenuWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalMenuWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack6 = new uGui.uGuiButton(SignalMenuWnd, uGui.uGuiButton.BTN_ID_18, 0, 42, 43, 52);
                    btnBack6.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack6.SetText("BACK");
                    btnBack6.SetStyle((byte)(btnBack6.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack6.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack6.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack6.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack6.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext6 = new uGui.uGuiButton(SignalMenuWnd, uGui.uGuiButton.BTN_ID_19, 45, 42, 86, 52);
                    btnNext6.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext6.SetText("NEXT");
                    btnNext6.SetStyle((byte)(btnNext6.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext6.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext6.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext6.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext6.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect6 = new uGui.uGuiButton(SignalMenuWnd, uGui.uGuiButton.BTN_ID_20, 88, 42, 127, 52);
                    btnSelect6.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect6.SetText("SELECT");
                    btnSelect6.SetStyle((byte)(btnSelect6.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect6.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect6.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect6.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect6.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalMenu = new uGui.uGuiListBox(SignalMenuWnd, uGui.uGuiListBox.LSB_ID_3, 2, 2, 125, 40);
                    lbxSignalMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignalMenu.AddItem("Status");
                    lbxSignalMenu.AddItem("Alerts");
                    lbxSignalMenu.AddItem("Config");
                    lbxSignalMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Signal Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Status Window

                try
                {
                    SignalStatusWnd = new uGui.uGuiWindow(SignalStatusWindowMessage);
                    SignalStatusWnd.SetTitleText("STATUS");
                    SignalStatusWnd.SetTitleHeight(10);
                    SignalStatusWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalStatusWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalStatusWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalStatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalStatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack7 = new uGui.uGuiButton(SignalStatusWnd, uGui.uGuiButton.BTN_ID_21, 0, 42, 43, 52);
                    btnBack7.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack7.SetText("BACK");
                    btnBack7.SetStyle((byte)(btnBack7.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack7.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack7.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack7.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack7.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet7 = new uGui.uGuiButton(SignalStatusWnd, uGui.uGuiButton.BTN_ID_42, 45, 42, 86, 52);
                    btnSet7.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSet7.SetText("SET");
                    btnSet7.SetStyle((byte)(btnSet7.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSet7.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSet7.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet7.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSet7.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalConnected = new uGui.uGuiCheckBox(SignalStatusWnd, uGui.uGuiCheckBox.CHB_ID_2, 2, 1, 124, 11);
                    SignalConnected.SetFont(uGui.uGuiFont.FONT_6X8);
                    SignalConnected.SetText("Connected");
                    SignalConnected.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalConnected.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlerted = new uGui.uGuiCheckBox(SignalStatusWnd, uGui.uGuiCheckBox.CHB_ID_3, 2, 14, 124, 24);
                    SignalAlerted.SetFont(uGui.uGuiFont.FONT_6X8);
                    SignalAlerted.SetText("Alerted");
                    SignalAlerted.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlerted.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalValue = new uGui.uGuiTextBox(SignalStatusWnd, uGui.uGuiTextBox.TXB_ID_8, 2, 27, 124, 39);
                    SignalValue.SetFont(uGui.uGuiFont.FONT_6X8);
                    SignalValue.SetAlignment(uGui.ALIGN_CENTER_LEFT);
                    SignalValue.SetText("Value: ");
                    SignalValue.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalValue.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Signal Status Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Alerted Signals List Window

                try
                {
                    AlertedSignalsWnd = new uGui.uGuiWindow(AlertedSignalsWindowMessage);
                    AlertedSignalsWnd.SetTitleText("ALERTED LIST");
                    AlertedSignalsWnd.SetTitleHeight(10);
                    AlertedSignalsWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    AlertedSignalsWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalsWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    AlertedSignalsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack8 = new uGui.uGuiButton(AlertedSignalsWnd, uGui.uGuiButton.BTN_ID_22, 0, 42, 43, 52);
                    btnBack8.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack8.SetText("BACK");
                    btnBack8.SetStyle((byte)(btnBack8.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack8.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack8.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack8.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack8.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext8 = new uGui.uGuiButton(AlertedSignalsWnd, uGui.uGuiButton.BTN_ID_23, 45, 42, 86, 52);
                    btnNext8.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext8.SetText("NEXT");
                    btnNext8.SetStyle((byte)(btnNext8.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext8.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext8.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext8.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext8.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect8 = new uGui.uGuiButton(AlertedSignalsWnd, uGui.uGuiButton.BTN_ID_24, 88, 42, 127, 52);
                    btnSelect8.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect8.SetText("SELECT");
                    btnSelect8.SetStyle((byte)(btnSelect8.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect8.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect8.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect8.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect8.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxAlertedSignals = new uGui.uGuiListBox(AlertedSignalsWnd, uGui.uGuiListBox.LSB_ID_4, 2, 2, 125, 40);
                    lbxAlertedSignals.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxAlertedSignals.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxAlertedSignals.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Alerted Signals List Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Alerted Signal Menu Window

                try
                {
                    SignalAlertedsWnd = new uGui.uGuiWindow(AlertedSignalListWindowMessage);
                    SignalAlertedsWnd.SetTitleText("");
                    SignalAlertedsWnd.SetTitleHeight(10);
                    SignalAlertedsWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertedsWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertedsWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertedsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertedsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack9 = new uGui.uGuiButton(SignalAlertedsWnd, uGui.uGuiButton.BTN_ID_25, 0, 42, 43, 52);
                    btnBack9.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack9.SetText("BACK");
                    btnBack9.SetStyle((byte)(btnBack9.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack9.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack9.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack9.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack9.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext9 = new uGui.uGuiButton(SignalAlertedsWnd, uGui.uGuiButton.BTN_ID_26, 45, 42, 86, 52);
                    btnNext9.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext9.SetText("NEXT");
                    btnNext9.SetStyle((byte)(btnNext9.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext9.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext9.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext9.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext9.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect9 = new uGui.uGuiButton(SignalAlertedsWnd, uGui.uGuiButton.BTN_ID_27, 88, 42, 127, 52);
                    btnSelect9.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect9.SetText("SELECT");
                    btnSelect9.SetStyle((byte)(btnSelect9.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect9.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect9.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect9.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect9.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalAlerteds = new uGui.uGuiListBox(SignalAlertedsWnd, uGui.uGuiListBox.LSB_ID_5, 2, 2, 125, 40);
                    lbxSignalAlerteds.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignalAlerteds.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalAlerteds.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Alerted Signal Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Alerted Signal Status Window

                try
                {
                    AlertedSignalStatusWnd = new uGui.uGuiWindow(AlertedSignalStatusWindowMessage);
                    AlertedSignalStatusWnd.SetTitleText("ALERT STATUS");
                    AlertedSignalStatusWnd.SetTitleHeight(10);
                    AlertedSignalStatusWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    AlertedSignalStatusWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalStatusWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalStatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    AlertedSignalStatusWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack10 = new uGui.uGuiButton(AlertedSignalStatusWnd, uGui.uGuiButton.BTN_ID_28, 0, 42, 43, 52);
                    btnBack10.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack10.SetText("BACK");
                    btnBack10.SetStyle((byte)(btnBack10.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack10.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack10.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack10.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack10.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalInfo = new uGui.uGuiTextBox(AlertedSignalStatusWnd, uGui.uGuiTextBox.TXB_ID_9, 1, 1, 126, 39);
                    AlertedSignalInfo.SetFont(uGui.uGuiFont.FONT_6X8);
                    AlertedSignalInfo.SetText("");
                    AlertedSignalInfo.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    AlertedSignalInfo.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Alerted Signal Status Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Alerts Menu Window

                try
                {
                    SignalAlertsWnd = new uGui.uGuiWindow(SignalAlertsListWindowMessage);
                    SignalAlertsWnd.SetTitleText("ALERTS");
                    SignalAlertsWnd.SetTitleHeight(10);
                    SignalAlertsWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertsWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertsWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertsWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack11 = new uGui.uGuiButton(SignalAlertsWnd, uGui.uGuiButton.BTN_ID_29, 0, 42, 43, 52);
                    btnBack11.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack11.SetText("BACK");
                    btnBack11.SetStyle((byte)(btnBack11.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack11.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack11.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack11.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack11.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext11 = new uGui.uGuiButton(SignalAlertsWnd, uGui.uGuiButton.BTN_ID_30, 45, 42, 86, 52);
                    btnNext11.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext11.SetText("NEXT");
                    btnNext11.SetStyle((byte)(btnNext11.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext11.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext11.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext11.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext11.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect11 = new uGui.uGuiButton(SignalAlertsWnd, uGui.uGuiButton.BTN_ID_31, 88, 42, 127, 52);
                    btnSelect11.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect11.SetText("SELECT");
                    btnSelect11.SetStyle((byte)(btnSelect11.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect11.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect11.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect11.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect11.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalAlerts = new uGui.uGuiListBox(SignalAlertsWnd, uGui.uGuiListBox.LSB_ID_6, 2, 2, 125, 40);
                    lbxSignalAlerts.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignalAlerts.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalAlerts.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Signal Alerts Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Alert Info Window

                try
                {
                    SignalAlertInfoWnd = new uGui.uGuiWindow(SignalAlertInfoWindowMessage);
                    SignalAlertInfoWnd.SetTitleText("ALERT INFO");
                    SignalAlertInfoWnd.SetTitleHeight(10);
                    SignalAlertInfoWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertInfoWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertInfoWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalAlertInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack12 = new uGui.uGuiButton(SignalAlertInfoWnd, uGui.uGuiButton.BTN_ID_32, 0, 42, 43, 52);
                    btnBack12.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack12.SetText("BACK");
                    btnBack12.SetStyle((byte)(btnBack10.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack12.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack12.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack12.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack12.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertInfo = new uGui.uGuiTextBox(SignalAlertInfoWnd, uGui.uGuiTextBox.TXB_ID_10, 1, 1, 126, 39);
                    SignalAlertInfo.SetFont(uGui.uGuiFont.FONT_6X8);
                    SignalAlertInfo.SetText("");
                    SignalAlertInfo.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalAlertInfo.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Signal Alert Info Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Config Menu Window

                try
                {
                    SignalConfigMenuWnd = new uGui.uGuiWindow(SignalConfigMenuWindowMessage);
                    SignalConfigMenuWnd.SetTitleText("SIGNAL CONFIG");
                    SignalConfigMenuWnd.SetTitleHeight(10);
                    SignalConfigMenuWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalConfigMenuWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalConfigMenuWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalConfigMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalConfigMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack13 = new uGui.uGuiButton(SignalConfigMenuWnd, uGui.uGuiButton.BTN_ID_33, 0, 42, 43, 52);
                    btnBack13.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack13.SetText("BACK");
                    btnBack13.SetStyle((byte)(btnBack13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack13.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack13.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack13.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack13.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext13 = new uGui.uGuiButton(SignalConfigMenuWnd, uGui.uGuiButton.BTN_ID_34, 45, 42, 86, 52);
                    btnNext13.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext13.SetText("NEXT");
                    btnNext13.SetStyle((byte)(btnNext13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext13.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext13.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext13.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext13.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect13 = new uGui.uGuiButton(SignalConfigMenuWnd, uGui.uGuiButton.BTN_ID_35, 88, 42, 127, 52);
                    btnSelect13.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect13.SetText("SELECT");
                    btnSelect13.SetStyle((byte)(btnSelect13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect13.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect13.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect13.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect13.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalConfigMenu = new uGui.uGuiListBox(SignalConfigMenuWnd, uGui.uGuiListBox.LSB_ID_7, 2, 2, 125, 40);
                    lbxSignalConfigMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignalConfigMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalConfigMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Info Field Window

                try
                {
                    SignalFieldInfoWnd = new uGui.uGuiWindow(SignalInfoFieldWindowMessage);
                    SignalFieldInfoWnd.SetTitleText("");
                    SignalFieldInfoWnd.SetTitleHeight(10);
                    SignalFieldInfoWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalFieldInfoWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalFieldInfoWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalFieldInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalFieldInfoWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack14 = new uGui.uGuiButton(SignalFieldInfoWnd, uGui.uGuiButton.BTN_ID_36, 0, 42, 43, 52);
                    btnBack14.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack14.SetText("BACK");
                    btnBack14.SetStyle((byte)(btnBack4.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack14.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack14.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack14.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack14.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtSignalField = new uGui.uGuiTextBox(SignalFieldInfoWnd, uGui.uGuiTextBox.TXB_ID_11, 1, 1, 126, 39);
                    txtSignalField.SetFont(uGui.uGuiFont.FONT_6X8);
                    txtSignalField.SetText("");
                    txtSignalField.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtSignalField.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Info Field Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Messages Menu Window

                try
                {
                    MessagesMenuWnd = new uGui.uGuiWindow(MessagesMenuWindowMessage);
                    MessagesMenuWnd.SetTitleText("MESSAGES");
                    MessagesMenuWnd.SetTitleHeight(10);
                    MessagesMenuWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    MessagesMenuWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    MessagesMenuWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    MessagesMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    MessagesMenuWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack15 = new uGui.uGuiButton(MessagesMenuWnd, uGui.uGuiButton.BTN_ID_37, 0, 42, 43, 52);
                    btnBack15.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack15.SetText("BACK");
                    btnBack15.SetStyle((byte)(btnBack13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack15.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack15.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack15.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack15.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext15 = new uGui.uGuiButton(MessagesMenuWnd, uGui.uGuiButton.BTN_ID_38, 45, 42, 86, 52);
                    btnNext15.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext15.SetText("NEXT");
                    btnNext15.SetStyle((byte)(btnNext13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext15.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext15.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext15.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext15.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect15 = new uGui.uGuiButton(MessagesMenuWnd, uGui.uGuiButton.BTN_ID_39, 88, 42, 127, 52);
                    btnSelect15.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect15.SetText("SELECT");
                    btnSelect15.SetStyle((byte)(btnSelect13.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect15.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect15.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect15.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect15.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxMessagesMenu = new uGui.uGuiListBox(MessagesMenuWnd, uGui.uGuiListBox.LSB_ID_8, 2, 2, 125, 40);
                    lbxMessagesMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxMessagesMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxMessagesMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Messages Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Send Message Window

                try
                {
                    SendMessageWnd = new uGui.uGuiWindow(SendMessageWindowMessage);
                    SendMessageWnd.SetTitleText("CONFIRM");
                    SendMessageWnd.SetTitleHeight(10);
                    SendMessageWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SendMessageWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SendMessageWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SendMessageWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SendMessageWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes3 = new uGui.uGuiButton(SendMessageWnd, uGui.uGuiButton.BTN_ID_40, 0, 42, 43, 52);
                    btnYes3.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnYes3.SetText("YES");
                    btnYes3.SetStyle((byte)(btnYes3.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnYes3.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnYes3.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes3.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnYes3.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo3 = new uGui.uGuiButton(SendMessageWnd, uGui.uGuiButton.BTN_ID_41, 88, 42, 127, 52);
                    btnNo3.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNo3.SetText("NO");
                    btnNo3.SetStyle((byte)(btnNo3.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNo3.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNo3.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo3.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNo3.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtMessage = new uGui.uGuiTextBox(SendMessageWnd, uGui.uGuiTextBox.TXB_ID_12, 1, 1, 126, 39);
                    txtMessage.SetFont(uGui.uGuiFont.FONT_6X8);
                    txtMessage.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    txtMessage.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Send Message Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion

                #region Signal Set Window

                try
                {
                    SignalSetWnd = new uGui.uGuiWindow(SignalSetWindowMessage);
                    SignalSetWnd.SetTitleText("SIGNAL SET");
                    SignalSetWnd.SetTitleHeight(10);
                    SignalSetWnd.SetTitleTextColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalSetWnd.SetTitleColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalSetWnd.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    SignalSetWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    SignalSetWnd.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack16 = new uGui.uGuiButton(SignalSetWnd, uGui.uGuiButton.BTN_ID_43, 0, 42, 43, 52);
                    btnBack16.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnBack16.SetText("BACK");
                    btnBack16.SetStyle((byte)(btnBack1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnBack16.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnBack16.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack16.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnBack16.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext16 = new uGui.uGuiButton(SignalSetWnd, uGui.uGuiButton.BTN_ID_44, 45, 42, 86, 52);
                    btnNext16.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnNext16.SetText("NEXT");
                    btnNext16.SetStyle((byte)(btnNext1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnNext16.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnNext16.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext16.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnNext16.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect16 = new uGui.uGuiButton(SignalSetWnd, uGui.uGuiButton.BTN_ID_45, 88, 42, 127, 52);
                    btnSelect16.SetFont(uGui.uGuiFont.FONT_5X8);
                    btnSelect16.SetText("SELECT");
                    btnSelect16.SetStyle((byte)(btnSelect1.GetStyle() | uGui.uGuiButton.BTN_STYLE_TOGGLE_COLORS));
                    btnSelect16.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    btnSelect16.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect16.SetAlternateForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    btnSelect16.SetAlternateBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalSetMenu = new uGui.uGuiListBox(SignalSetWnd, uGui.uGuiListBox.LSB_ID_9, 2, 2, 125, 40);
                    lbxSignalSetMenu.SetFont(uGui.uGuiFont.FONT_6X8);
                    lbxSignalSetMenu.AddItem("Set OFF");
                    lbxSignalSetMenu.AddItem("Set ON");
                    lbxSignalSetMenu.SetForeColor(new uGui.uGuiColor(uGui.uGuiColor.C_WHITE));
                    lbxSignalSetMenu.SetBackColor(new uGui.uGuiColor(uGui.uGuiColor.C_BLACK));
                    lbxSignalSetMenu.SetIndex(0);
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("InitGraphicObjects Menu Window Error:" + ex.Message + "|" + ex.StackTrace);
                }

                #endregion
                */

                Start = DateTime.UtcNow;
                LoadStatusWindow();
                Diff = DateTime.UtcNow - Start;
                Debug.WriteLine("Load Status Window: " + Diff.TotalMilliseconds.ToString("F2"));
            }

        }

        private static void LoadStatusWindow()
        {
            lock (SyncGraphics)
            {
                try
                {
                    DateTime Start = DateTime.UtcNow;
                    StatusWndTitle = DateTime.UtcNow.ToString("yy/MM/dd HH:mm");
                    StatusWnd.SetTitleText(StatusWndTitle);
                    StatusWnd.SetTitleTextAlignment(ALIGN_CENTER);
                    StatusWnd.Show();
                    TimeSpan Diff = DateTime.UtcNow - Start;
                    Debug.WriteLine("Load Status Window Show: " + Diff.TotalMilliseconds.ToString("F2"));
                    //NetworkControl.NetworkConnectInfo st = NetworkControl.NetworkConnectionStatus();
                    Start = DateTime.UtcNow;
                    NetworkConnected.Checked(true);
                    BrokerConnected.Checked(false);
                    IpAddr.SetText("IP: 0.0.0.0");
                    uGui.Update();
                    Diff = DateTime.UtcNow - Start;
                    Debug.WriteLine("Load Status Window Update: " + Diff.TotalMilliseconds.ToString("F2"));
                    //LcdStandByTimeOut = ushort.MaxValue;
                    //Lcd.DisplayUpdate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("LoadStatusWindow Error:" + ex.Message + "|" + ex.StackTrace);
                }
            }
        }

        /*private void LoadMenuWindow(bool ResetIndex = true)
        {
            lock (SyncGraphics)
            {
                try
                {
                    MenuWnd.Show();
                    if (ResetIndex)
                    {
                        lbxMenu.SetIndex(0);
                    }
                    uGui.Update();
                }
                catch (Exception ex)
                {
                    AppDebug.WriteLine("LoadMenuWindow Error:" + ex.Message + "|" + ex.StackTrace);
                }
            }
        }*/

        private static void StatusWindowMessage(uGui.uGuiMessage msg)
        {
            try
            {
                Debug.WriteLine("Status Window Message, Type: " + msg.type.ToString() + ", Id: " + msg.id + ", SubId: " + msg.sub_id + ", Event: " + msg.evt + ", Source: " + msg.src);
                switch (msg.type)
                {
                    case uGui.uGuiMessage.MSG_TYPE_OBJECT:
                        switch (msg.id)
                        {
                            case uGui.uGuiObject.OBJ_TYPE_BUTTON:
                                if ((msg.evt == uGui.uGuiObject.OBJ_EVENT_PRESSED) || (msg.evt == uGui.uGuiObject.OBJ_EVENT_RELEASED))
                                {
                                    //LcdStandByTimeOut = LCD_STAND_BY_TIME_OUT;
                                }
                                switch (msg.sub_id)
                                {
                                    case uGui.uGuiButton.BTN_ID_0:
                                        if (msg.evt == uGui.uGuiObject.OBJ_EVENT_RELEASED)
                                        {
                                            Debug.WriteLine("Menu Button Event");
                                            //new Thread(() => LoadMenuWindow()).Start();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("StatusWindowMessage Error:" + ex.Message + "|" + ex.StackTrace);
            }
        }
    }
}
