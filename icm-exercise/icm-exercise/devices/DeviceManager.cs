using System;
using System.Collections.Generic;
using System.Linq;

namespace icm_exercise.devices
{
    public class DeviceManager
    {
        internal Dictionary<string, IDevice> Devices = new Dictionary<string, IDevice>();

        public void RegisterDevice(IDevice device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            Devices[device.Id] = device;
        }

        public IEnumerable<IDevice> AllDevices()
        {
            return Devices.Values.AsEnumerable();
        }

        public IDevice GetDevice(string id)
        {
            if (Devices.TryGetValue(id, out var device))
            {
                return device;
            }
            return null;
        }

        public void RemoveDevice(IDevice device)
        {
            RemoveDevice(device.Id);
        }

        public void RemoveDevice(string id)
        {
            if (Devices.ContainsKey(id))
            {
                Devices.Remove(id);
            }
        }
    }
}
