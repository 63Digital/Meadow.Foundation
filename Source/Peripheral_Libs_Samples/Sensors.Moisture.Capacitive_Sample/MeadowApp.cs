﻿using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Sensors.Moisture;

namespace Sensors.Moisture.Capacitive_Sample
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        Capacitive capacitive;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            capacitive = new Capacitive(
                analogPort: Device.CreateAnalogInputPort(Device.Pins.A01),
                minimumVoltageCalibration: 2.84f,
                maximumVoltageCalibration: 1.63f
            );
            capacitive.Updated += CapacitiveUpdated;

            TestCapacitiveUpdating();
            //TestCapacitiveRead();
        }

        void CapacitiveUpdated(object sender, FloatChangeResult e)
        {
            Console.WriteLine($"Raw: {capacitive.Moisture} | Moisture {(int)(e.New * 100)}%");
        }

        void TestCapacitiveUpdating() 
        {
            Console.WriteLine("TestCapacitiveUpdating...");

            capacitive.StartUpdating();
        }

        void TestCapacitiveRead()
        {
            Console.WriteLine("TestCapacitiveSensor...");

            // Use Read(); to get soil moisture value from 0f - 1f
            while (true)
            {
                float moisture = capacitive.Read().Result;

                if (moisture > 1.0f)
                    moisture = 1.0f;
                else 
                if (moisture < 0)
                    moisture = 0;

                Console.WriteLine($"Raw: {capacitive.Moisture} | Moisture {(int) (moisture * 100)}%");
                Thread.Sleep(1000);
            }
        }
    }
}