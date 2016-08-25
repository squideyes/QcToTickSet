using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QcToTickSet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoWork();
            }
            catch (Exception error)
            {
                Console.WriteLine();
                Console.WriteLine("Error: " + error.Message);
            }

            Console.WriteLine();
            Console.Write("Press any key to terminate...");

            Console.ReadKey(true);
        }

        private static void DoWork()
        {
            foreach (var source in Directory.GetFiles(
                Properties.Settings.Default.Source,
                "*.csv", SearchOption.AllDirectories))
            {
                var nameOnly = Path.GetFileNameWithoutExtension(source);

                var parts = nameOnly.Split('_');

                var d = DateTime.ParseExact(parts[0], "yyyyMMdd", null);

                var date = new DateTime(
                    d.Year, d.Month, d.Day, 0, 0, 0, DateTimeKind.Utc);

                var symbol = (Symbol)Enum.Parse(typeof(Symbol), parts[1], true);

                using (var reader = new StreamReader(source))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var fields = line.Split(',');

                        var ms = int.Parse(fields[0]);

                        var tick = new Tick()
                        {
                            Symbol = symbol,
                            TickOn = date.AddMilliseconds(ms).ToEstFromUtc(),
                            BidRate = double.Parse(fields[1]),
                            AskRate = double.Parse(fields[2])
                        };
                    }
                }
            }
        }
    }
}
