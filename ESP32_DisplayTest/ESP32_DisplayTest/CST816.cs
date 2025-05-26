using nanoFramework.Hardware.Esp32;
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;

namespace ESP32_DisplayTest
{
    internal class CST816
    {
        private const byte CST816T_ADDRESS = 0x15;

        private const byte REG_GESTURE_ID = 0x01;
        private const byte REG_FINGER_NUM = 0x02;
        private const byte REG_XPOS_H = 0x03;
        private const byte REG_XPOS_L = 0x04;
        private const byte REG_YPOS_H = 0x05;
        private const byte REG_YPOS_L = 0x06;
        private const byte REG_CHIP_ID = 0xA7;
        private const byte REG_PROJ_ID = 0xA8;
        private const byte REG_FW_VERSION = 0xA9;
        private const byte REG_FACTORY_ID = 0xAA;
        private const byte REG_SLEEP_MODE = 0xE5;
        private const byte REG_IRQ_CTL = 0xFA;
        private const byte REG_LONG_PRESS_TICK = 0xEB;
        private const byte REG_MOTION_MASK = 0xEC;
        private const byte REG_DIS_AUTOSLEEP = 0xFE;

        private const byte MOTION_MASK_CONTINUOUS_LEFT_RIGHT = 0b100;
        private const byte MOTION_MASK_CONTINUOUS_UP_DOWN = 0b010;
        private const byte MOTION_MASK_DOUBLE_CLICK = 0b001;

        private const byte IRQ_EN_TOUCH = 0x40;
        private const byte IRQ_EN_CHANGE = 0x20;
        private const byte IRQ_EN_MOTION = 0x10;
        private const byte IRQ_EN_LONGPRESS = 0x01;

        public enum TouchState
        {
            TS_NONE = 0,
            TS_PRESSED,
            TS_RELEASED
        }

        private int SclPin = -1;
        private int SdaPin = -1;
        private GpioPin RstPin = null;
        private I2cDevice Port = null;
        private byte ChipId = 0;
        private byte FirmwareVersion = 0;
        public byte GestureId { get; protected set; } = 0;
        public byte FingerNum { get; protected set; } = 0;
        public ushort X { get; protected set; } = 0;
        public ushort Y { get; protected set; } = 0;

        public bool Pressed { get; protected set; } = false;

        public enum TouchPadMode
        {
            MODE_TOUCH,
            MODE_CHANGE,
            MODE_FAST,
            MODE_MOTION
        }

        public CST816(int sclPin, int sdaPin, GpioPin rstPin = null)
        {
            SclPin = sclPin;
            SdaPin = sdaPin;
            RstPin = rstPin;
        }

        public void Init(TouchPadMode tpMode)
        {
            if (Port == null)
            {
                if (SclPin >= 0)
                {
                    Configuration.SetPinFunction(SclPin, DeviceFunction.I2C1_CLOCK);
                }
                if (SdaPin >= 0)
                {
                    Configuration.SetPinFunction(SdaPin, DeviceFunction.I2C1_DATA);
                }
                Port = I2cDevice.Create(new I2cConnectionSettings(
                    1,
                    CST816T_ADDRESS,
                    I2cBusSpeed.FastMode));
                if(RstPin != null)
                {
                    RstPin.Write(PinValue.High);
                    Thread.Sleep(100);
                    RstPin.Write(PinValue.Low);
                    Thread.Sleep(100);
                    RstPin.Write(PinValue.High);
                    Thread.Sleep(100);
                }
                byte[] Cmd = new byte[1];
                Cmd[0] = REG_CHIP_ID;
                byte[] Data = new byte[4];
                SpanByte Tx = new SpanByte(Cmd);
                SpanByte Rx = new SpanByte(Data);
                I2cTransferResult Res = Port.WriteRead(Tx, Rx);
                if(Res.Status == I2cTransferStatus.FullTransfer)
                {
                    ChipId = Data[0];
                    FirmwareVersion = Data[3];
                }
                byte irq_en = 0x0;
                byte motion_mask = 0x0;
                switch (tpMode)
                {
                    case TouchPadMode.MODE_TOUCH:
                        irq_en = IRQ_EN_TOUCH;
                        break;
                    case TouchPadMode.MODE_CHANGE:
                        irq_en = IRQ_EN_CHANGE;
                        break;
                    case TouchPadMode.MODE_FAST:
                        irq_en = IRQ_EN_MOTION;
                        break;
                    case TouchPadMode.MODE_MOTION:
                        irq_en = IRQ_EN_MOTION | IRQ_EN_LONGPRESS;
                        motion_mask = MOTION_MASK_DOUBLE_CLICK;
                        break;
                    default:
                        break;
                }
                Cmd = new byte[2];
                Cmd[0] = REG_IRQ_CTL;
                Cmd[0] = irq_en;
                Tx = new SpanByte(Cmd);
                Port.Write(Tx);
                Cmd = new byte[2];
                Cmd[0] = REG_MOTION_MASK;
                Cmd[0] = motion_mask;
                Tx = new SpanByte(Cmd);
                Port.Write(Tx);
            }
        }

        public TouchState Read()
        {
            TouchState Res = TouchState.TS_NONE;
            if(Port != null)
            {
                byte[] Cmd = new byte[1];
                Cmd[0] = REG_GESTURE_ID;
                byte[] Data = new byte[6];
                SpanByte Tx = new SpanByte(Cmd);
                SpanByte Rx = new SpanByte(Data);
                I2cTransferResult Result = Port.WriteRead(Tx, Rx);
                if(Result.Status == I2cTransferStatus.FullTransfer)
                {
                    GestureId = Data[0];
                    FingerNum = Data[1];
                    X = (ushort)((((ushort)Data[2] & 0x0f) << 8) | (ushort)Data[3]);
                    Y = (ushort)((((ushort)Data[4] & 0x0f) << 8) | (ushort)Data[5]);
                    if (FingerNum > 0)
                    {
                        Res = TouchState.TS_PRESSED;
                        Pressed = true;
                    }
                    else
                    {
                        if (Pressed)
                        {
                            Pressed = false;
                            Res = TouchState.TS_RELEASED;
                        }
                    }
                }
                else
                {
                    if(Pressed)
                    {
                        Pressed = false;
                        Res = TouchState.TS_RELEASED;
                    }
                }
            }
            return Res;
        }
    }
}
