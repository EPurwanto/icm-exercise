namespace icm_exercise.devices
{
    /// <summary>
    /// Generic controller interface for all devices. Any connected device will have an associated controller that
    /// implements this interface.
    /// Other methods that might be included:
    /// - Summary giving an overview of the device state
    /// - Status for if the device is online/offline
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// The unique identifier for this device
        /// </summary>
        string Id { get; }

        /// <summary>
        /// What category of device it is, eg CAM for camera.
        /// </summary>
        string Category { get; }
    }
}
