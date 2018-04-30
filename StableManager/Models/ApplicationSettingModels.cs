using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class ApplicationSettingModels
    {
    }

    public class NumberingSystem
    {
        public string NumberringSystemID { get; set; }

        public string ClientNumberFormat { get; set; }
        public string UserNumberFormat { get; set; }
    }
}
