using icm_exercise.devices.cameras;
using Xunit;

namespace icm_exercise_tests.devices.cameras
{
    public class SimulatedCameraTest
    {
        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(5, 6, 11)]
        [InlineData(5, -2, 3)]
        [InlineData(5, -6, -1)]
        public async void PanTest(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Pan =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pan = await cam.Pan(move);
            Assert.Equal(expected, pan, 2);
        }

        [Theory]
        [InlineData(0, 5, -5)]
        [InlineData(5, 6, -1)]
        [InlineData(5, -2, 7)]
        [InlineData(5, -6, 11)]
        public async void PanTest_Inverted(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Pan =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pan = await cam.Pan(move, false);
            Assert.Equal(expected, pan, 2);
        }
        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(5, 6, 11)]
        [InlineData(5, -2, 3)]
        [InlineData(5, -6, -1)]
        public async void PitchTest(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Pitch =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pitch = await cam.Pitch(move);
            Assert.Equal(expected, pitch, 2);
        }

        [Theory]
        [InlineData(0, 5, -5)]
        [InlineData(5, 6, -1)]
        [InlineData(5, -2, 7)]
        [InlineData(5, -6, 11)]
        public async void PitchTest_Inverted(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Pitch =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pitch = await cam.Pitch(move, false);
            Assert.Equal(expected, pitch, 2);
        }

        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(5, 6, 11)]
        [InlineData(5, -2, 3)]
        [InlineData(5, -6, -1)]
        public async void TiltTest(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Tilt =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pitch = await cam.Tilt(move);
            Assert.Equal(expected, pitch, 2);
        }

        [Theory]
        [InlineData(0, 5, -5)]
        [InlineData(5, 6, -1)]
        [InlineData(5, -2, 7)]
        [InlineData(5, -6, 11)]
        public async void TiltTest_Inverted(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Tilt =  initial
            });

            var cam = new SimulatedCamera(channel);
            var pitch = await cam.Tilt(move, false);
            Assert.Equal(expected, pitch, 2);
        }

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(0, 3, 3)]
        [InlineData(0, 4, 3)]
        [InlineData(1, 1, 2)]
        [InlineData(2, -1, 1)]
        [InlineData(2, -2, 0)]
        [InlineData(2, -4, 0)]
        public async void ZoomTest(double initial, double move, double expected)
        {
            var channel = new CameraCommChannel(new CameraState()
            {
                Zoom = initial
            });

            var cam = new SimulatedCamera(channel);
            var zoom = await cam.Zoom(move);
            Assert.Equal(expected, zoom, 2);
        }
    }
}
