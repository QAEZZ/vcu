using System;
using VCU.Commands;

namespace VCU;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowHelp();
            return;
        }

        if (args[0].ToLower() != "help" && args[0].ToLower() != "init" && !Directory.Exists(".vcu"))
        {
            Console.WriteLine("A VCU repository has not been initialized in the currect directory.\nYou can initialize one by running: vcu init main");
            return;
        }

        switch (args[0].ToLower())
        {
            case "config": Config.Execute(args); break;
            case "init": Init.Execute(args); break;
            case "teardown": Teardown.Execute(args); break;
            case "remote": Remote.Execute(args); break;
            default: ShowHelp(); break;
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Usage: vcu <command> [arguments]\n\nCommands:\n    help : this.\n    init : Initialize a branch");
    }
}
