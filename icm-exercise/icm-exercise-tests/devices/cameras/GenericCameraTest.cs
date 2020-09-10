using System;
using System.Linq;
using System.Threading.Tasks;
using icm_exercise.devices;
using icm_exercise.devices.cameras;
using Moq;
using Xunit;

namespace icm_exercise_tests.devices.cameras
{
    public class GenericCameraTest
    {
        [Fact]
        public void TestId()
        {
            var moq = new Mock<GenericCamera>("test");
            Assert.Equal("test", moq.Object.Id);
        }

        [Fact]
        public void TestPan_Supported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Pan);
            moq.Setup(cam => cam.DoPan(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var task = moq.Object.Pan(3);

            task.Wait();

            Assert.Null(task.Exception);
        }

        [Fact]
        public void TestPan_NotSupported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(0);
            moq.Setup(cam => cam.DoPan(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var ex = Assert.Throws<AggregateException>(moq.Object.Pan(3).Wait);
            var notSupported = ex.InnerExceptions.Any(e => typeof(NotSupportedException).IsInstanceOfType(e));
            Assert.True(notSupported);
        }

        [Fact]
        public void TestPitch_Supported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Pitch);
            moq.Setup(cam => cam.DoPitch(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var task = moq.Object.Pitch(3);

            task.Wait();

            Assert.Null(task.Exception);
        }

        [Fact]
        public void TestPitch_NotSupported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(0);
            moq.Setup(cam => cam.DoPitch(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var ex = Assert.Throws<AggregateException>(moq.Object.Pitch(3).Wait);
            var notSupported = ex.InnerExceptions.Any(e => typeof(NotSupportedException).IsInstanceOfType(e));
            Assert.True(notSupported);
        }

        [Fact]
        public void TestTilt_Supported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Tilt);
            moq.Setup(cam => cam.DoTilt(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var task = moq.Object.Tilt(3);

            task.Wait();

            Assert.Null(task.Exception);
        }

        [Fact]
        public void TestTilt_NotSupported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(0);
            moq.Setup(cam => cam.DoTilt(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var ex = Assert.Throws<AggregateException>(moq.Object.Tilt(3).Wait);
            var notSupported = ex.InnerExceptions.Any(e => typeof(NotSupportedException).IsInstanceOfType(e));
            Assert.True(notSupported);
        }

        [Fact]
        public void TestZoom_Supported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Zoom);
            moq.Setup(cam => cam.DoTilt(3.0, true)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var task = moq.Object.Zoom(3);

            task.Wait();

            Assert.Null(task.Exception);
        }

        [Fact]
        public void TestZoom_NotSupported()
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.SupportedMotions).Returns(0);
            moq.Setup(cam => cam.DoZoom(3.0)).Returns(Task<double>.Factory.StartNew(() => 2.0));

            var ex = Assert.Throws<AggregateException>(moq.Object.Zoom(3).Wait);
            var notSupported = ex.InnerExceptions.Any(e => typeof(NotSupportedException).IsInstanceOfType(e));
            Assert.True(notSupported);
        }

        [Theory]
        [InlineData(4, 2, 1, 1)]
        [InlineData(4, 2, 2, 2)]
        [InlineData(4, 2, 3, 2)]
        [InlineData(4, 2, -1, -1)]
        [InlineData(4, 2, -2, -2)]
        [InlineData(4, 2, -3, -2)]
        public void TestZoom_BoundClipping(double max, double initial, double amount, double expected)
        {
            var moq = new Mock<GenericCamera>("test");
            moq.Setup(cam => cam.MaxZoom).Returns(max);
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Zoom);
            moq.Setup(cam => cam.DoZoom(It.IsAny<double>())).Returns((double i) => Task<double>.Factory.StartNew(() => i));

            var mockCam = moq.Object;
            mockCam.State = new CameraState()
            {
                Zoom = initial
            };

            var task = mockCam.Zoom(amount);
            task.Wait();

            Assert.Null(task.Exception);
            Assert.Equal(expected, task.Result, 2);
        }

        [Theory]
        // Trivial case
        [InlineData(0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0)]
        // Single motions
        [InlineData(0, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0)]
        [InlineData(0, 0, 0, 0,
            0, 1, 0, 0,
            0, 1, 0, 0)]
        [InlineData(0, 0, 0, 0,
            0, 0, 1, 0,
            0, 0, 1, 0)]
        [InlineData(0, 0, 0, 0,
            0, 0, 0, 1,
            0, 0, 0, 1)]
        // Single motions from starting point
        [InlineData(2, 0, 0, 0,
            3, 0, 0, 0,
            1, 0, 0, 0)]
        [InlineData(0, 2, 0, 0,
            0, 3, 0, 0,
            0, 1, 0, 0)]
        [InlineData(0, 0, 2, 0,
            0, 0, 3, 0,
            0, 0, 1, 0)]
        [InlineData(0, 0, 0, 2,
            0, 0, 0, 3,
            0, 0, 0, 1)]
        // Single Reverse motions from starting point
        [InlineData(3, 0, 0, 0,
            2, 0, 0, 0,
            -1, 0, 0, 0)]
        [InlineData(0, 3, 0, 0,
            0, 2, 0, 0,
            0, -1, 0, 0)]
        [InlineData(0, 0, 3, 0,
            0, 0, 2, 0,
            0, 0, -1, 0)]
        [InlineData(0, 0, 0, 3,
            0, 0, 0, 2,
            0, 0, 0, -1)]
        // Grouped motions
        [InlineData(0, 0, 0, 0,
            1, 1, 1, 1,
            1, 1, 1, 1)]
        [InlineData(2, 2, 2, 2,
            3, 3, 3, 3,
            1, 1, 1, 1)]
        [InlineData(3, 3, 3, 3,
            2, 2, 2, 2,
            -1, -1, -1, -1)]
        public void TestLookAt(double initialPan,  double initialPitch,  double initialTilt,  double initialZoom,
            double targetPan,   double targetPitch,   double targetTilt,   double targetZoom,
            double expectedPan, double expectedPitch, double expectedTilt, double expectedZoom)
        {
            var moq = new Mock<GenericCamera>("test");
            var movement = new CameraState();

            moq.Setup(cam => cam.MaxZoom).Returns(4);
            moq.Setup(cam => cam.SupportedMotions).Returns(CameraMoveType.Pan | CameraMoveType.Pitch | CameraMoveType.Tilt | CameraMoveType.Zoom);

            moq.Setup(cam => cam.DoPan(It.IsAny<double>(), true))
                .Callback((double i, bool t) => movement.Pan = i);

            moq.Setup(cam => cam.DoPitch(It.IsAny<double>(), true))
                .Callback((double i, bool t) => movement.Pitch = i);

            moq.Setup(cam => cam.DoTilt(It.IsAny<double>(), true))
                .Callback((double i, bool t) => movement.Tilt = i);

            moq.Setup(cam => cam.DoZoom(It.IsAny<double>()))
                .Callback((double i) => movement.Zoom = i);


            var mockCam = moq.Object;
            mockCam.State = new CameraState()
            {
                Pan = initialPan,
                Pitch = initialPitch,
                Tilt = initialTilt,
                Zoom = initialZoom
            };;

            var task = mockCam.LookAt(targetPan, targetPitch, targetTilt, targetZoom);
            task.Wait();

            Assert.Equal(expectedPan, movement.Pan);
            Assert.Equal(expectedPitch, movement.Pitch);
            Assert.Equal(expectedTilt, movement.Tilt);
            Assert.Equal(expectedZoom, movement.Zoom);
        }
    }
}
