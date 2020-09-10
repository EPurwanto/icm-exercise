using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("icm-exercise-tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace icm_exercise.devices.cameras
{
    /// <summary>
    /// This is a generalised camera controller, allowing us to treat any model of camera in the same way. Subclasses
    /// adapt specific models to behave appropriately
    /// </summary>
    public abstract class GenericCamera
    {
        internal CameraState State;

        // where maxZoom + 1 = the magnification multiplier.
        public virtual double MaxZoom => 0;

        public virtual CameraMoveType SupportedMotions => 0;

        /// <summary>
        /// Pan the camera left or right
        /// </summary>
        /// <param name="amount">how far to pan the camera. Can be negative.</param>
        /// <param name="right">Whether to pan left (false) or right (true). Defaults to true.</param>
        /// <returns>The current facing direction of the camera in degrees.</returns>
        public async Task<double> Pan(double amount, bool right = true)
        {
            if (!SupportedMotions.HasFlag(CameraMoveType.Pan))
            {
                throw  new NotSupportedException();
            }
            return await DoPan(amount, right);
        }

        /// <summary>
        /// Camera specific implementation of pan motion
        /// </summary>
        internal abstract Task<double> DoPan(double amount, bool right = true);

        /// <summary>
        /// Pitch the camera up or down
        /// </summary>
        /// <param name="amount">how far to pitch the camera. Can be negative.</param>
        /// <param name="right">Whether to pitch up (true) or down (false). Defaults to true.</param>
        /// <returns>The current pitch of the camera in degrees.</returns>
        public async Task<double> Pitch(double amount, bool up = true)
        {
            if (!SupportedMotions.HasFlag(CameraMoveType.Pitch))
            {
                throw  new NotSupportedException();
            }
            return await DoPitch(amount, up);
        }

        /// <summary>
        /// Camera specific implementation of pitch motion
        /// </summary>
        internal abstract Task<double> DoPitch(double amount, bool up = true);

        /// <summary>
        /// Tilt the camera clockwise or counterclockwise
        /// </summary>
        /// <param name="amount">how much to tilt the camera. Can be negative.</param>
        /// <param name="right">Whether to pan clockwise (true) or counterclockwise (false). Defaults to true.</param>
        /// <returns>The current tilt of the camera in degrees.</returns>
        public async Task<double> Tilt(double amount, bool clockwise = true)
        {
            if (!SupportedMotions.HasFlag(CameraMoveType.Tilt))
            {
                throw  new NotSupportedException();
            }
            return await DoTilt(amount, clockwise);
        }


        /// <summary>
        /// Camera specific implementation of tilt motion
        /// </summary>
        internal abstract Task<double> DoTilt(double amount, bool clockwise = true);

        /// <summary>
        /// Tilt the camera clockwise or counterclockwise
        /// </summary>
        /// <param name="amount">how much to tilt the camera. Can be negative.</param>
        /// <param name="right">Whether to pan clockwise (true) or counterclockwise (false). Defaults to true.</param>
        /// <returns>The current tilt of the camera in degrees.</returns>
        public async Task<double> Zoom(double amount)
        {
            if (!SupportedMotions.HasFlag(CameraMoveType.Zoom))
            {
                throw  new NotSupportedException();
            }

            if (amount < 0 && -amount > State.Zoom)
            { // Floor zoom at 0;
                amount = -State.Zoom;
            }
            else if (amount > 0 && State.Zoom + amount > MaxZoom)
            { // Cap zoom at maxZoom
                amount = MaxZoom - State.Zoom;
            }

            return await DoZoom(amount);
        }


        /// <summary>
        /// Camera specific implementation of tilt motion
        /// </summary>
        internal abstract Task<double> DoZoom(double amount);

        public async Task<CameraState> LookAt(double? pan, double? pitch, double? tilt, double? zoom)
        {
            List<Task<double>> tasks = new List<Task<double>>();
            if (pan.HasValue)
            {

            }

            return State;
        }
    }

    public struct CameraState
    {
        public double Pan;
        public double Pitch;
        public double Tilt;
        public double Zoom;
    }

    [Flags]
    public enum CameraMoveType
    {
        Pan = 1,
        Pitch = 2,
        Tilt = 4,
        Zoom = 8
    }
}
