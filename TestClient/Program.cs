using System;
using System.Threading;

namespace TestClient
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            using (var source = new CancellationTokenSource(new TimeSpan(0, 0, 0, 230)))
            {
                var test = new AuthTest();

                test.StartTest().Wait(source.Token);
            }

            Console.WriteLine(@"Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
