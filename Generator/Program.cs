using System;
using Generator;

namespace SkeletonApplicationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Files...");
            CodeGenerator.MakeFiles(Int32.Parse(args[0]));
            Console.WriteLine("Done generating Files");
        }
    }
}
