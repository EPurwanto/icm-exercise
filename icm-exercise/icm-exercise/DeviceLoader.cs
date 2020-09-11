﻿using System.Collections;
using System.Collections.Generic;
using icm_exercise.devices.cameras;

namespace icm_exercise.devices
{
    public class DeviceLoader
    {
        public IEnumerable<IDevice> LoadConnectedDevices()
        {
            // This would look at some saved config to see what devices to initialise, and how to connect to them.
            // For now this is just creating the test camera

            var simulatedCamera = new SimulatedCamera("CAMERA_ID", new CameraCommChannel(new CameraState()));
            yield return simulatedCamera;
        }
    }
}
