using System;
using Generator;

namespace SkeletonApplicationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Files...");
            CodeGenerator.MakeFiles();
            Console.WriteLine("Done generating Files");
        }
    }
}
