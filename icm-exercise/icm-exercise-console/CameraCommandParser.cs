﻿using System;
using System.Collections.Generic;
using System.Linq;
using icm_exercise.devices;

namespace icm_exercise_console
{
    /// <summary>
    /// Handles command line inputs when a camera is selected.
    /// </summary>
    public class CameraCommandParser: DeviceCommandParser
    {
        public override string Parse(string command, ref DeviceState state)
        {
            var cam = state.SelectedDevice as ICamera;
            if (cam == null)
                throw new ArgumentException("Selected Device is null or not a camera.");

            var err = base.Parse(command, ref state);
            double val = 0;
            var strings = command.Split(" ");

            try
            {
                // If base has handled the command, all of these should fall through.
                // If it hasn't, then we will get the "Not Recognised" message
                switch (strings[0].ToLower())
                {
                    case "pan":
                        if (TryParse(strings[1], out val, out err))
                        {
                            return Pan(cam, val);
                        }

                        break;
                    case "pitch":
                        if (TryParse(strings[1], out val, out err))
                        {
                            return Pitch(cam, val);
                        }

                        break;
                    case "tilt":
                        if (TryParse(strings[1], out val, out err))
                        {
                            return Tilt(cam, val);
                        }

                        break;
                    case "zoom":
                        if (TryParse(strings[1], out val, out err))
                        {
                            return Zoom(cam, val);
                        }

                        break;
                    case "lookat":
                        if (TryParse(strings.Skip(1).ToArray(), out var values, out err))
                        {
                            return LookAt(cam, values);
                        }

                        break;
                }
            }
            catch (NotSupportedException e)
            {
                err = "Operation is not supported on this camera.";
            }

            return err;
        }

        public override string HelpText()
        {
            return @"The following commands are available:
lookat [pan]? [pitch]? [tilt]? [zoom]? - Set the camera to the given pan, pitch, tilt and zoom levels.
pan [amount] - Turn the camera [amount] degrees to the right. Amount can be negative.
pitch [amount] - Turn the camera [amount] degrees up. Amount can be negative.
quit - Exit the program
tilt [amount] - Turn the camera [amount] degrees clockwise. Amount can be negative.
zoom [amount] - Zoom the camera [amount]x magnification. Amount can be negative.";
        }

        public string Pan(ICamera cam, double amount)
        {
            var task = cam.Pan(amount);
            task.Wait();

            return $"Position {cam.CurrentState().ToString()}";
        }

        public string Pitch(ICamera cam, double amount)
        {
            var task = cam.Pitch(amount);
            task.Wait();

            return $"Position {cam.CurrentState().ToString()}";
        }

        public string Tilt(ICamera cam, double amount)
        {
            var task = cam.Tilt(amount);
            task.Wait();

            return $"Position {cam.CurrentState().ToString()}";
        }

        public string Zoom(ICamera cam, double amount)
        {
            var task = cam.Zoom(amount);
            task.Wait();

            return $"Position {cam.CurrentState().ToString()}";
        }

        public string LookAt(ICamera cam, double[] positions)
        {
            var task = cam.LookAt(SafeGetIndex(positions, 0),
                SafeGetIndex(positions, 1),
                SafeGetIndex(positions, 2),
                SafeGetIndex(positions, 3));

            task.Wait();

            return $"Position {cam.CurrentState().ToString()}";
        }

        public bool TryParse(string s, out double value, out string error)
        {
            if (double.TryParse(s, out double val))
            {
                value = val;
                error = null;
                return true;
            }
            else
            {
                value = 0;
                error = $"Could not parse {s} into a number";
                return false;
            }
        }

        public bool TryParse(string[] strings, out double[] values, out string error)
        {
            var list = new List<double>();

            foreach (var s in strings)
            {
                if (TryParse(s, out var value, out error))
                {
                    list.Add(value);
                }
                else
                {
                    values = new double[0];
                    return false;
                }
            }

            values = list.ToArray();
            error = null;
            return true;
        }

        private double? SafeGetIndex(double[] values, int index)
        {
            if (values.Length > index)
            {
                return values[index];
            }

            return null;
        }
    }
}
