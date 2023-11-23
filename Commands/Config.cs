using System;
using System.IO;
using VCU.Constants;

namespace VCU.Commands;

internal static class Config
{
    internal static void Execute(string[] args) {

        if (args.Length < 3) {
            Console.WriteLine("Error: Invalid number of arguments.");
            return;
        }

        string toConfig = args[1];
        string configValue = args[2];

        if (!ConfigConstants.allowedConfigTreePaths.Contains(toConfig)) {
            Console.WriteLine("Error: You cannot edit that key or the key was not found.");
            return;
        }

        string[] toConfigSplit = toConfig.Split("/");

        string configParent = toConfigSplit[0];
        string configChild = toConfigSplit[1];
    }
}