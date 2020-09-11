using icm_exercise.devices;

namespace icm_exercise_console
{
    public interface ICommandParser
    {
        string Parse(string command, ref DeviceState state);
        string HelpText();
    }
}
