﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Hardware;
using Meadow.Foundation;
using Meadow.Foundation.LEDs;

namespace Hello_RGB
{
    public class RGBApp : AppBase<F7Micro, RGBApp>
    {
        protected bool _running;

        protected RgbPwmLed _onboardLed;

        public RGBApp()
        {
            Console.WriteLine("Got here.");

            var foo = Device.Pins.D01;

            // new up our onboard RGB LED
            _onboardLed = new RgbPwmLed(
                new PWMPort(Device.Pins.OnboardLEDRed), 
                new PWMPort(Device.Pins.OnboardLEDGreen), 
                new PWMPort(Device.Pins.OnboardLEDBlue));

            // wherever you're at, you can get the current instance:
            // var foo = RGBApp.Current;
        }

        public override void Run()
        {
            StartRunningColors();
        }

        public void StartRunningColors()
        {
            _running = true;

            Task stuff = new Task(async () => {
                while (_running)
                {
                    // loop through the entire hue spectrum (360 degrees)
                    for (int i = 0; i < 360; i++)
                    {
                        var hue = ((double)i / 360F);
                        Debug.Print(hue.ToString());

                        // set the color of the RGB
                        // TODO TODO TODO TODO: Need to add extension method for this
                        //_onboardLed.SetColor(Color.FromHsba(((double)i / 360F), 1, 1));

                        // for a fun, fast rotation through the hue spectrum:
                        //await Task.Delay(1);
                        // for a gentle walk through the forest of colors;
                        await Task.Delay(18);
                    }
                }
            });
            stuff.Start();
        }
    }
}
