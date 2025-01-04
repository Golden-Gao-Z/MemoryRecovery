using Microsoft.Extensions.Configuration;
using NPOI.HSSF.Record.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.WpfClient
{

    /// <summary>
    /// todo: config class should be configurated as a service like in Asp.Net 
    /// </summary>
    public class TempConfig
    {
        private static object @lock = new();
        private static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.Development.json", false, true).Build();
        private TempConfig() { }

        private static TempConfig? instance;

        public static TempConfig GetInstance()
        {
            if (instance == null)
            {
                lock (@lock)
                {
                    instance ??= new TempConfig();
                }
            }
            return instance;
        }

        public IEnumerable<string> FilesSources
        {
            get
            {
                // read files paths strings.
                var foo = configuration.GetSection("FilesSources")
                    .GetChildren()
                    .Select(tt =>
                    {
                        if (string.Compare(tt.GetSection("Disabled").Value, "false", StringComparison.OrdinalIgnoreCase) == 0)
                            return tt.GetSection("Path").Value ?? string.Empty;
                        return string.Empty;
                    }
                    ).Where(tt=>!string.IsNullOrEmpty(tt));
                return foo;
            }
        }

    }
}
