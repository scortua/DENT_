using System;
using System.Threading;
using System.Diagnostics;
using System.Device.Adc;
using System.Device.Dac;

namespace AN_DI
{
    public class Program
    {
        public static void Main()
        {
            // Create an ADC controller
            AdcController adc2 = new AdcController();
      
            int max = adc2.MaxValue;
            int min = adc2.MinValue;

            Debug.WriteLine("max=" + max.ToString() + " min=" + min.ToString() + "\n\n");
            Thread.Sleep(2000);
            // Create an ADC channel
            AdcChannel ac2 = adc2.OpenChannel(4);

            while (true)
            {
                int value = ac2.ReadValue();
                float voltaje = value * 3.3f / 4096;
                double percent = ac2.ReadRatio();

                Debug.WriteLine("value0=" + value.ToString() + " ratio=" + percent.ToString() + " Voltaje=" + voltaje);

                Thread.Sleep(1000);
            }
            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
