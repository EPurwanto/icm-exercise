Generic Device controller for an interview challenge.

The repository consists of 2 source projects and two associated test projects:
* icm-exercise contains the bulk of the logic, and is designed to be interface agnostic
* icm-exercise-console is the command line application serving as the interface

Comments are provided explaining the function of specific classes but the overall architecture is explained below. 

Devices are represented by a controller that is an instance of IDevice. Specific categories of device, such as Cameras will have their own controller interface. 
Implementations of these controller interfaces will allow us to treat any specific device in a general way, abstracting away the details of controlling that device.

Each category of device should have it's own ICommandParser implementation that handles the command line arguments for controlling devices of that kind. These parses
can be swapped in and out easily depending on the selected device, allowing each device category to have a discrete set of controls. 