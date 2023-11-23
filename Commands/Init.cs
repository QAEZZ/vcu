using System;
using System.IO;
using VCU.Constants;

namespace VCU.Commands
{
    internal static class Init
    {
        internal static void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Missing branch name for 'init' command.");
                return;
            }

            string branch = args[1];

            Directory.CreateDirectory("./.vcu");
            Directory.CreateDirectory("./.vcu/remotes");

            File.WriteAllText(ConfigConstants.configFilePath, ConfigConstants.ConfigYamlContent(branch));

            Console.WriteLine($"Initialized VCU repository.\nBranch: {branch}\n..Path: {Path.GetFullPath(".vcu")}");
        }
    }
}
