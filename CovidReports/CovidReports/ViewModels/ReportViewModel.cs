using CovidReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidReports.ViewModels
{
    public class ReportViewModel
    {
        public Report<Region> Regions { get; set; }
        //public List<Customer> Customers { get; set; }
        public IEnumerable<RegionReportViewModel> Reports { get; set; }

        public string ReportTitle { get; set; }
    }
}