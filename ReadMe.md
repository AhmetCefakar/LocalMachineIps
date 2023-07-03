# IP Address Service

This code provides a C# implementation for retrieving internal and external IP addresses using the `NetworkInterface` class. It includes an interface `IIpAddressService` and its implementation `IpAddressService` for fetching IP addresses, along with a `Program` class for executing the code.

## Features

- Retrieves internal IP addresses
- Retrieves external IP addresses
- Filters out loopback and non-operational network interfaces
- Determines if an IP address is internal or external based on certain IP ranges

## Prerequisites

To run this code, ensure that you have the following:

- .NET Framework installed
- An IDE or text editor to compile and run the code

## Usage

1. Open the source code file in your preferred IDE or text editor.
2. Compile and run the program.

## Code Structure

The code is structured as follows:

- `IIpAddressService` interface: Defines the methods for retrieving IP addresses.
- `IpAddressService` class: Implements the `IIpAddressService` interface and provides methods for retrieving IP addresses.
- `Program` class: Acts as the entry point of the program and demonstrates the usage of the `IpAddressService` class.
  - `Run` method: Calls the IP address service methods and prints the retrieved IP addresses.
  - `PrintIpAddresses` method: Helper method to print the IP addresses.
  - `Main` method: Creates an instance of the IP address service, creates an instance of the `Program` class, and runs the program.

## Customization

You can customize the code according to your needs:

- Modify the IP range in the `IsInternalIp` method to match your internal IP range if necessary.
- Extend the `IIpAddressService` interface or `IpAddressService` class to include additional functionality or IP address retrieval methods.

## License

This code is provided under the [MIT License](https://opensource.org/licenses/MIT). Feel free to modify and use it according to your requirements.

## Contributing

If you would like to contribute to this code, feel free to submit a pull request with your suggested changes or improvements.

## Acknowledgments

This code is based on the use of the `NetworkInterface` class and the concept of determining internal and external IP addresses.