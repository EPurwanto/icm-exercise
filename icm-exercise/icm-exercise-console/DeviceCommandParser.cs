namespace icm_exercise_console
{
    /// <summary>
    /// Base class for handling command line input when a device is selected. Parses the 'help' and 'deselect' commands
    /// although an implementation of HelpText must be provided.
    /// </summary>
    public abstract class DeviceCommandParser : ICommandParser
    {
        public virtual string Parse(string command, ref DeviceState state)
        {
            var strings = command.Split(" ");

            double val = 0;
            string err = "Command not recognised, type 'help' to see all commands.";
            switch (strings[0].ToLower())
            {
                case "help":
                    return HelpText();
                case "deselect":
                    return Deselect(ref state);
            }

            return err;
        }

        public abstract string HelpText();

        public string Deselect(ref DeviceState state)
        {
            var device = state.SelectedDevice;

            state.SelectedDevice = null;
            state.Parser = new DefaultCommandParser();
            return $"Deselected {device.Id}.";
        }
    }
}
