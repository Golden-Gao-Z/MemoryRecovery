namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ulong ii = 0;
            while (true)
            {
                Thread.Sleep(10);
                ii++;
                Task.Run(() =>
                {
                    throw new Exception();
                });

            }
        }
    }
}
