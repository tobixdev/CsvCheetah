using BenchmarkDotNet.Running;

namespace CsvCheetah.Benchmarks
{
    public class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}