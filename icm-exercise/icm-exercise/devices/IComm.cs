using System.Threading.Tasks;

namespace icm_exercise.devices
{
    /// <summary>
    /// Generic connection interface. This is just a stub for the real connection protocol
    /// </summary>
    /// <typeparam name="DeviceState"></typeparam>
    public interface IComm<DeviceState>
    {
        Task SendCommand(DeviceState nextState);
    }
}
