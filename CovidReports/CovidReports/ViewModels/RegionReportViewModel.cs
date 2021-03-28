using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidReports.ViewModels
{
    public class RegionReportViewModel
    {
        public string Name { get; set; }
        public long? Confirmed { get; set; }
        public long? Deadths { get; set; }
    }
}