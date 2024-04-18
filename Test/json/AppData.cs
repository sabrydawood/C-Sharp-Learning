using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.json
{

    internal class AppData
    {
        public string AppName { get; set; } = "TestApp";
        public string AppVersion { get; set; } = "1.0.0.0";

        public override string ToString()
        {
            return $"{AppName} {AppVersion}";
        }
        
    }
}
