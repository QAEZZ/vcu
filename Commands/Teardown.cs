using System;
using System.IO;

namespace VCU.Commands
{
    internal static class Teardown
    {
        public static void Execute(string[] args)
        {
            // Add actual logic to check if a VCU repo has been initialized.
            Directory.Delete(".vcu");

            Console.WriteLine($"Tore down VCU repository.\nPath..: {Path.GetFullPath(".vcu")}");
        }
    }
}
