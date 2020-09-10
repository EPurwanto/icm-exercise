using System;
using System.Threading.Tasks;

namespace icm_exercise.devices.cameras
{
    /// <summary>
    /// This controller simulates control over an external camera.
    /// </summary>
    public class SimulatedCamera : GenericCamera
    {
        private CameraCommChannel _channel;
        public override double MaxZoom => 3;

        public override CameraMoveType SupportedMotions => CameraMoveType.Pan | CameraMoveType.Pitch | CameraMoveType.Tilt | CameraMoveType.Zoom;

        public SimulatedCamera(CameraCommChannel channel)
        {
            _channel = channel;
            State = _channel.State;
        }

        internal override async Task<double> DoPan(double amount, bool right = true)
        {
            var directionFix = right ? 1 : -1;

            await _channel.SendCommand(new CameraState()
            {
                Pan = State.Pan + amount * directionFix
            });


            State.Pan = _channel.State.Pan % 360;

            return State.Pan;
        }

        internal override async Task<double> DoPitch(double amount, bool up = true)
        {
            var directionFix = up ? 1 : -1;

            await _channel.SendCommand(new CameraState()
            {
                Pitch = State.Pitch + amount * directionFix
            });


            State.Pitch = _channel.State.Pitch % 360;

            return State.Pitch;
        }

        internal override async Task<double> DoTilt(double amount, bool clockwise = true)
        {
            var directionFix = clockwise ? 1 : -1;

            await _channel.SendCommand(new CameraState()
            {
                Tilt = State.Tilt + amount * directionFix
            });


            State.Tilt = _channel.State.Tilt % 360;

            return State.Tilt;
        }

        internal override async Task<double> DoZoom(double amount)
        {
            await _channel.SendCommand(new CameraState()
            {
                Zoom = State.Zoom + amount
            });


            State.Zoom = _channel.State.Zoom;

            return State.Zoom;
        }
    }
}
