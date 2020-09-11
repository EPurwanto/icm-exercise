using icm_exercise.devices;

namespace icm_exercise_console
{
    /// <summary>
    /// Handles command line inputs, allowing commands for specific devices to be isolated and swapped between.
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Parses a single command.
        /// </summary>
        /// <param name="command">The input from the command line</param>
        /// <param name="state">The current program state</param>
        /// <returns>A message to be output to the command line</returns>
        string Parse(string command, ref DeviceState state);

        /// <summary>
        /// Every implementation must include the 'help' command, detailing the possible inputs
        /// </summary>
        /// <returns>A list of the possible command inputs</returns>
        string HelpText();
    }
}
