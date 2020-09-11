using System.Threading;
using System.Threading.Tasks;

namespace icm_exercise.devices.cameras
{
    /// <summary>
    /// This fakes communication with a networked camera. This is just a stub for the real connection protocol
    /// </summary>
    public class CameraCommChannel: IComm<CameraState>
    {
        public CameraState State
        {
            get;
            private set;
        }
        private int simulatedDelay;

        public CameraCommChannel(CameraState initialState, int simulatedDelay = 150)
        {
            State = initialState;
            this.simulatedDelay = simulatedDelay;
        }

        public async Task SendCommand(CameraState nextState)
        {
            Thread.Sleep(simulatedDelay);
            State = nextState;
        }
    }
}
