using icm_exercise.devices;
using icm_exercise.devices.cameras;
using icm_exercise_console;
using Xunit;

namespace icm_exercise_console_tests
{
    public class CameraCommandParserTest
    {
        private DeviceState DefaultState()
        {
            var deviceManager = new DeviceManager();
            var commandParser = new CameraCommandParser();
            var cam = new SimulatedCamera("TEST", new CameraCommChannel(new CameraState(), 0));

            return new DeviceState()
            {
                DeviceManager = deviceManager,
                Parser = commandParser,
                SelectedDevice = cam
            };
        }

        [Fact]
        public void TestInvalidCommand()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "This is not a real command";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, state.Parser);
            Assert.Equal("Command not recognised, type 'help' to see all commands.", output);
        }

        [Fact]
        public void TestHelp()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "help";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.StartsWith("The following commands are available:", output);

            command = "HELP";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.StartsWith("The following commands are available:", output);
        }

        [Fact]
        public void TestPan()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "pan 15";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (15, 0, 0, 0)", output);

            // Reset state

            state = DefaultState();
            commandParser = state.Parser;
            cam = state.SelectedDevice;

            command = "PAN 15";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (15, 0, 0, 0)", output);
        }

        [Fact]
        public void TestPitch()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "pitch 15";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 15, 0, 0)", output);

            // Reset state

            state = DefaultState();
            commandParser = state.Parser;
            cam = state.SelectedDevice;

            command = "PITCH 15";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 15, 0, 0)", output);
        }

        [Fact]
        public void TestTilt()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "tilt 15";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 0, 15, 0)", output);

            // Reset state

            state = DefaultState();
            commandParser = state.Parser;
            cam = state.SelectedDevice;

            command = "TILT 15";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 0, 15, 0)", output);
        }

        [Fact]
        public void TestZoom()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "zoom 2";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 0, 0, 2)", output);

            // Reset state

            state = DefaultState();
            commandParser = state.Parser;
            cam = state.SelectedDevice;

            command = "ZOOM 2";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (0, 0, 0, 2)", output);
        }

        [Fact]
        public void TestLookAt()
        {
            var state = DefaultState();
            var commandParser = state.Parser;
            var cam = state.SelectedDevice;

            var command = "lookat 15 20 25 2";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (15, 20, 25, 2)", output);

            // Reset state

            state = DefaultState();
            commandParser = state.Parser;
            cam = state.SelectedDevice;

            command = "LOOKAT 15 20 25 2";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("Position (15, 20, 25, 2)", output);
        }

        [Fact]
        public void TestDeselect()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var command = "deselect";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.NotEqual(commandParser, state.Parser);
            Assert.Equal("Deselected TEST.", output);
        }
    }
}
