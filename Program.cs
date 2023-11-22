using System;

namespace VCU
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("arguments:");

                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"Argument: {args[i]}");
                }
            }
            else
            {
                Console.WriteLine("No arguments provided.");
            }
        }
    }
}