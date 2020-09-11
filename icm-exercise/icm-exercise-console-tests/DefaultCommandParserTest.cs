using icm_exercise.devices;
using icm_exercise.devices.cameras;
using icm_exercise_console;
using Moq;
using Xunit;

namespace icm_exercise_console_tests
{
    public class DefaultCommandParserTest
    {
        private DeviceState DefaultState()
        {
            var deviceManager = new DeviceManager();
            var commandParser = new DefaultCommandParser();

            return new DeviceState()
            {
                DeviceManager = deviceManager,
                Parser = commandParser,
                SelectedDevice = null
            };
        }

        [Fact]
        public void TestInvalidCommand()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var command = "This is not a real command";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, state.Parser);
            Assert.Equal("Command not recognised, type 'help' to see all commands.", output);
        }

        [Fact]
        public void TestHelp()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var command = "help";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.StartsWith("The following commands are available:", output);

            command = "HELP";
            output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.StartsWith("The following commands are available:", output);
        }

        [Fact]
        public void TestListDevices_Empty()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var command = "list";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("No devices are connected.", output);

            command = "LIST";
            output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal("No devices are connected.", output);
        }

        [Fact]
        public void TestListDevices_Camera()
        {
            var state = DefaultState();
            state.DeviceManager.RegisterDevice(new SimulatedCamera("TEST", new CameraCommChannel(new CameraState())));
            var commandParser = state.Parser;

            var command = "list";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal(@"Type ID
CAM  TEST", output);

            command = "LIST";
            output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal(@"Type ID
CAM  TEST", output);
        }

        [Fact]
        public void TestListDevices_Unknown()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var moq = new Mock<IDevice>();
            moq.Setup(d => d.Id).Returns("DVC_ID");
            state.DeviceManager.RegisterDevice(moq.Object);

            var command = "list";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal(@"Type ID
UNKN DVC_ID
", output);

            command = "LIST";
            output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, commandParser);
            Assert.Equal(@"Type ID
UNKN DVC_ID
", output);
        }

        [Fact]
        public void TestSelect_Valid()
        {
            var state = DefaultState();
            var cam = new SimulatedCamera("TEST", new CameraCommChannel(new CameraState()));
            state.DeviceManager.RegisterDevice(cam);
            var commandParser = state.Parser;

            var command = "select TEST";
            var output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.NotEqual(commandParser, state.Parser);
            Assert.Equal("Selected TEST.", output);

            // Clear state changes

            state = DefaultState();
            cam = new SimulatedCamera("TEST", new CameraCommChannel(new CameraState()));
            state.DeviceManager.RegisterDevice(cam);
            commandParser = state.Parser;

            command = "SELECT TEST";
            output = commandParser.Parse(command, ref state);

            Assert.Equal(cam, state.SelectedDevice);
            Assert.NotEqual(commandParser, state.Parser);
            Assert.Equal("Selected TEST.", output);
        }

        [Fact]
        public void TestSelect_Invalid()
        {
            var state = DefaultState();
            var commandParser = state.Parser;

            var command = "select TEST";
            var output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, state.Parser);
            Assert.Equal("Device TEST not found.", output);

            // Clear state changes

            state = DefaultState();
            commandParser = state.Parser;

            command = "SELECT TEST";
            output = commandParser.Parse(command, ref state);

            Assert.Null(state.SelectedDevice);
            Assert.Equal(commandParser, state.Parser);
            Assert.Equal("Device TEST not found.", output);
        }
    }
}
