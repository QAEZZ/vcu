using System;
using System.IO;

namespace VCU.Commands;

internal static class Teardown
{
    internal static void Execute(string[] args)
    {
        
        if (!Directory.Exists(".vcu")) {
            Console.WriteLine("A VCU repository has not been initialized in the currect directory.\nYou can initialize one by running: vcu init main");
            return;
        }

        Directory.Delete(".vcu", true);

        Console.WriteLine($"Tore down VCU repository.\n..Path: {Path.GetFullPath(".vcu")}");
    }
}