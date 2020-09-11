using System;
using System.Text;
using icm_exercise.devices;

namespace icm_exercise_console
{
    /// <summary>
    /// Handles command line input when no device is selected
    /// </summary>
    public class DefaultCommandParser: ICommandParser
    {
        public string Parse(string command, ref DeviceState state)
        {
            var strings = command.Split(" ");

            switch (strings[0].ToLower())
            {
                case "help":
                    return HelpText();
                case "list":
                    return ListDevices(state);
                case "select":
                    return SelectDevice(strings[1], ref state);
            }

            return "Command not recognised, type 'help' to see all commands.";
        }

        public string HelpText()
        {
            return @"The following commands are available:
list - List all connected devices
quit - Exit the program
select [id] - Select the device with the given ID";
        }

        public string ListDevices(DeviceState state)
        {
            var sb = new StringBuilder();

            foreach (var device in state.DeviceManager.AllDevices())
            {
                sb.Append(Environment.NewLine).Append(device.Category).Append(" ").Append(device.Id);
            }


            if (sb.Length > 0)
            {
                sb.Insert(0, "Type ID");
                return sb.ToString();
            }
            return "No devices are connected.";
        }

        private string SelectDevice(string id, ref DeviceState state)
        {
            var device = state.DeviceManager.GetDevice(id);

            if (device != null)
            {
                state.SelectedDevice = device;

                switch (device)
                {
                    case ICamera cam:
                        state.Parser = new CameraCommandParser();
                        break;
                }

                return $"Selected {device.Id}.";
            }

            return $"Device {id} not found.";
        }
    }
}
