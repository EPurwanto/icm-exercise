using icm_exercise.devices.cameras;
using Xunit;

namespace icm_exercise_tests.devices.cameras
{
    public class GenericCameraTest
    {
        [Theory]
        [InlineData(0, 5, 5)]
        public void PanTest(double initial, double move, double expected)
        {
            var cam = new GenericCamera();
            var pan = cam.Pan(move);
            Assert.Equal(expected, pan, 2);
        }
    }
}
