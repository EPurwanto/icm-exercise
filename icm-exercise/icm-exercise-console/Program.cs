﻿using System;
 using icm_exercise.devices;

 namespace icm_exercise_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Devices");
            var deviceManager = new DeviceManager();
            foreach (var device in new DeviceLoader().LoadConnectedDevices())
            {
                Console.WriteLine($"{device.Id} Loaded");
                deviceManager.RegisterDevice(device);
            }

            Console.WriteLine("Devices Ready");
            var state = new DeviceState()
            {
                DeviceManager = deviceManager,
                Parser = new DefaultCommandParser(),
                SelectedDevice = null
            };

            while (true)
            {
                Console.Write($"{state.SelectedDevice?.Id ?? ""}% ");
                var line = Console.ReadLine();
                if (line.Trim().ToLower() == "quit")
                {
                    return;
                }

                var message = state.Parser.Parse(line, ref state);

                Console.WriteLine(message);
            }
        }
    }

    public struct DeviceState
    {
        public DeviceManager DeviceManager;
        public ICommandParser Parser;
        public IDevice SelectedDevice;
    }
}
