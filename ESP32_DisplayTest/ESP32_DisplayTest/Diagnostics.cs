﻿using nanoFramework.Hardware.Esp32;
using System;
using System.Diagnostics;

namespace ESP32_DisplayTest.Helpers
{
    public class Diagnostics
    {
        public static void PrintMemory(string msg)
        {
            NativeMemory.GetMemoryInfo(NativeMemory.MemoryType.SpiRam, out uint totalSize, out uint totalFree, out uint largestFree);
            Debug.WriteLine($"{msg} -> Internal Mem:  Total Internal: {totalSize} Free: {totalFree} Largest: {largestFree}");
            //Debug.WriteLine($"nF Mem:  {nanoFramework.Runtime.Native.GC.Run(false)}");
        }
    }
}
