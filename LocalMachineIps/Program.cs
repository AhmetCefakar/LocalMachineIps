using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

// Interface defining the methods for retrieving IP addresses
public interface IIpAddressService
{
    IEnumerable<IPAddress> GetInternalIpAddresses();
    IEnumerable<IPAddress> GetExternalIpAddresses();
}

// Implementation of the IP address service
public class IpAddressService : IIpAddressService
{
    public IEnumerable<IPAddress> GetInternalIpAddresses()
    {
        return GetIpAddresses(IsInternalIp);
    }

    public IEnumerable<IPAddress> GetExternalIpAddresses()
    {
        return GetIpAddresses(ip => !IsInternalIp(ip));
    }

    private IEnumerable<IPAddress> GetIpAddresses(Func<IPAddress, bool> filter)
    {
        var ipAddresses = new List<IPAddress>();

        // Get all network interfaces
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface ni in interfaces)
        {
            // Filter out loopback and non-operational interfaces
            if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                IPInterfaceProperties ipProperties = ni.GetIPProperties();

                // Get the IPv4 addresses for the network interface
                var addresses = ipProperties.UnicastAddresses
                    .Where(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(addr => addr.Address)
                    .Where(filter);

                ipAddresses.AddRange(addresses);
            }
        }

        return ipAddresses;
    }

    // Determines if the given IP address is an internal IP address
    private bool IsInternalIp(IPAddress ipAddress)
    {
        byte[] addressBytes = ipAddress.GetAddressBytes();
        if (addressBytes[0] == 10
            || (addressBytes[0] == 172 && addressBytes[1] >= 16 && addressBytes[1] <= 31)
            || (addressBytes[0] == 192 && addressBytes[1] == 168))
        {
            return true;
        }

        return false;
    }
}

// Entry point of the program
public class Program
{
    private readonly IIpAddressService ipAddressService;

    public Program(IIpAddressService ipAddressService)
    {
        this.ipAddressService = ipAddressService;
    }

    public void Run()
    {
        Console.WriteLine("Internal IP Addresses:");
        IEnumerable<IPAddress> internalIpAddresses = ipAddressService.GetInternalIpAddresses();
        PrintIpAddresses(internalIpAddresses);

        Console.WriteLine("\nExternal IP Addresses:");
        IEnumerable<IPAddress> externalIpAddresses = ipAddressService.GetExternalIpAddresses();
        PrintIpAddresses(externalIpAddresses);

        Console.ReadLine();
    }

    // Helper method to print the IP addresses
    private void PrintIpAddresses(IEnumerable<IPAddress> ipAddresses)
    {
        foreach (IPAddress ipAddress in ipAddresses)
        {
            Console.WriteLine(ipAddress);
        }
    }

    public static void Main()
    {
        // Create an instance of the IP address service
        IIpAddressService ipAddressService = new IpAddressService();

        // Create an instance of the program and run it
        Program program = new Program(ipAddressService);
        program.Run();
    }
}

