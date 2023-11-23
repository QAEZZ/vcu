using System;
using System.IO;

namespace VCU.Commands;

internal static class Teardown
{
    internal static void Execute(string[] args)
    {

        Directory.Delete(".vcu", true);

        Console.WriteLine($"Tore down VCU repository.\n..Path: {Path.GetFullPath(".vcu")}");
    }
}