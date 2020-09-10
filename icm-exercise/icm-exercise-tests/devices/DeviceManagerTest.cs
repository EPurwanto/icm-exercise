using icm_exercise.devices;
using icm_exercise.devices.cameras;
using Moq;
using Xunit;

namespace icm_exercise_tests.devices
{
    public class DeviceManagerTest
    {
        [Fact]
        public void TestRegisterDevice()
        {
            var deviceManager = new DeviceManager();

            var returnedDevice = deviceManager.GetDevice("test");
            Assert.Null(returnedDevice);


            var moq = new Mock<IDevice>();
            moq.Setup(device => device.Id).Returns("test");
            deviceManager.RegisterDevice(moq.Object);


            returnedDevice = deviceManager.GetDevice("test");
            Assert.Equal(moq.Object, returnedDevice);
        }

        [Fact]
        public void TestRemoveDevice_Object()
        {
            var deviceManager = new DeviceManager();

            var returnedDevice = deviceManager.GetDevice("test");
            Assert.Null(returnedDevice);

            var moq = new Mock<IDevice>();
            moq.Setup(device => device.Id).Returns("test");
            deviceManager.RegisterDevice(moq.Object);


            returnedDevice = deviceManager.GetDevice("test");
            Assert.Equal(moq.Object, returnedDevice);

            deviceManager.RemoveDevice(moq.Object);
            returnedDevice = deviceManager.GetDevice("test");
            Assert.Null(returnedDevice);
        }

        [Fact]
        public void TestRemoveDevice_Id()
        {
            var deviceManager = new DeviceManager();

            var returnedDevice = deviceManager.GetDevice("test");
            Assert.Null(returnedDevice);

            var moq = new Mock<IDevice>();
            moq.Setup(device => device.Id).Returns("test");
            deviceManager.RegisterDevice(moq.Object);


            returnedDevice = deviceManager.GetDevice("test");
            Assert.Equal(moq.Object, returnedDevice);

            deviceManager.RemoveDevice("test");
            returnedDevice = deviceManager.GetDevice("test");
            Assert.Null(returnedDevice);
        }

        [Fact]
        public void TestAllDevices()
        {
            var deviceManager = new DeviceManager();

            Assert.Empty(deviceManager.AllDevices());

            var moq1 = new Mock<IDevice>();
            moq1.Setup(device => device.Id).Returns("test1");
            deviceManager.RegisterDevice(moq1.Object);

            Assert.Contains(moq1.Object, deviceManager.AllDevices());


            var moq2 = new Mock<IDevice>();
            moq2.Setup(device => device.Id).Returns("test2");
            deviceManager.RegisterDevice(moq2.Object);

            Assert.Contains(moq1.Object, deviceManager.AllDevices());
            Assert.Contains(moq2.Object, deviceManager.AllDevices());
        }
    }
}
