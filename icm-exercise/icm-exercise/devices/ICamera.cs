using System;
using System.Threading.Tasks;
using icm_exercise.devices.cameras;

namespace icm_exercise.devices
{
    public interface ICamera : IDevice
    {
        double MaxZoom { get; }
        CameraMoveType SupportedMotions { get; }

        CameraState CurrentState();

        /// <summary>
        /// Pan the camera left or right
        /// </summary>
        /// <param name="amount">how far to pan the camera. Can be negative.</param>
        /// <param name="right">Whether to pan left (false) or right (true). Defaults to true.</param>
        /// <returns>The current facing direction of the camera in degrees.</returns>
        Task<double> Pan(double amount, bool right = true);

        /// <summary>
        /// Pitch the camera up or down
        /// </summary>
        /// <param name="amount">how far to pitch the camera. Can be negative.</param>
        /// <param name="right">Whether to pitch up (true) or down (false). Defaults to true.</param>
        /// <returns>The current pitch of the camera in degrees.</returns>
        Task<double> Pitch(double amount, bool up = true);

        /// <summary>
        /// Tilt the camera clockwise or counterclockwise
        /// </summary>
        /// <param name="amount">how much to tilt the camera. Can be negative.</param>
        /// <param name="right">Whether to pan clockwise (true) or counterclockwise (false). Defaults to true.</param>
        /// <returns>The current tilt of the camera in degrees.</returns>
        Task<double> Tilt(double amount, bool clockwise = true);


        /// <summary>
        /// Tilt the camera clockwise or counterclockwise
        /// </summary>
        /// <param name="amount">how much to tilt the camera. Can be negative.</param>
        /// <param name="right">Whether to pan clockwise (true) or counterclockwise (false). Defaults to true.</param>
        /// <returns>The current tilt of the camera in degrees.</returns>
        Task<double> Zoom(double amount);

        /// <summary>
        /// Point the camera at a specific direction.
        /// </summary>
        /// <param name="pan">the target direction's pan coordinate</param>
        /// <param name="pitch">the target direction's pitch coordinate</param>
        /// <param name="tilt">the target direction's tilt coordinate</param>
        /// <param name="zoom">the target zoom level</param>
        /// <returns>The final state of the camera</returns>
        Task<CameraState> LookAt(double? pan, double? pitch, double? tilt, double? zoom);
    }

    public struct CameraState
    {
        public double Pan;
        public double Pitch;
        public double Tilt;
        public double Zoom;

        public override string ToString()
        {
            return $"({Pan}, {Pitch}, {Tilt}, {Zoom})";
        }
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
