using System;
using System.IO;

namespace VCU.Commands
{
    internal static class Init
    {
        public static void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error: Missing branch name for 'init' command.");
                return;
            }

            string branch = args[1];

            Directory.CreateDirectory(".vcu");

            Console.WriteLine($"Initialized VCU repository.\nBranch: {branch}\nPath..: {Path.GetFullPath(".vcu")}");
        }
    }
}
