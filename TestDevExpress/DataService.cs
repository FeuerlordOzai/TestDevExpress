using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDevExpress
{
    public class DataService
    {
        public List<OilData> GetOilData()
        {
            Console.WriteLine("Fetching data from DataService");
            return new List<OilData>
            {
                new OilData { Date = new DateTime(2020, 1, 1), Price = 680 },
                new OilData { Date = new DateTime(2021, 1, 1), Price = 720 },
                new OilData { Date = new DateTime(2022, 1, 1), Price = 730 },
                new OilData { Date = new DateTime(2023, 1, 1), Price = 750 },
                new OilData { Date = new DateTime(2024, 1, 1), Price = 735 },
                new OilData { Date = new DateTime(2024, 6, 1), Price = 740 }
            };
        }
    }
}
