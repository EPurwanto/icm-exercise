using System.Threading.Tasks;

namespace icm_exercise.devices
{
    public interface IComm<DeviceState>
    {
        Task SendCommand(DeviceState nextState);
    }
}
