using System;
using VCU.Commands;

namespace VCU
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            switch (args[0])
            {
                case "init": Init.Execute(args); break;
                case "teardown": Teardown.Execute(args); break;
                default: ShowHelp(); break;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: vcu <command> [arguments]\n\nCommands:\n    help : this.\n    init : Initialize a branch");
        }
    }
}