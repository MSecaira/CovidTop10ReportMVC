using CovidReports.Models;
using CovidReports.Utils;
using CovidReports.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CovidReports.Controllers
{
    public class ReportController : Controller
    {
        private readonly IService _reportService;
        public ReportController()
        {
            _reportService = ReportService.Instance;
        }
        // GET: Report
        public async System.Threading.Tasks.Task<ActionResult> Index(string regions)
        {
            ViewBag.SelectedRegion = regions;
            ReportViewModel reportAndRegions = new ReportViewModel();
            var request = _reportService.GetHttpRequestMessage(ReportType.REGION, null);
            var jsonString = await _reportService.ExecuteGet(request);
            ReportGenerator reportGenerator = new ReportGenerator();
            reportAndRegions.Regions = reportGenerator.GetRegions(jsonString);


            if (string.IsNullOrWhiteSpace(regions))
            {
                request = _reportService.GetHttpRequestMessage(ReportType.REPORT, null);
                jsonString = await _reportService.ExecuteGet(request);
                reportAndRegions.ReportTitle = "REGION";
                reportAndRegions.Reports = reportGenerator.GetRegionReport(jsonString);
            }
            else
            {
                request = _reportService.GetHttpRequestMessage(ReportType.REPORT, regions);
                jsonString = await _reportService.ExecuteGet(request);
                reportAndRegions.ReportTitle = "PROVINCE";
                reportAndRegions.Reports = reportGenerator.GetProvinceReport(jsonString);
            }
            reportGenerator.GetXMLReport(reportAndRegions.Reports.ToList());
            return View(reportAndRegions);
        }

        public async System.Threading.Tasks.Task<FileResult> GetReport(string hparam, string submitButton)
        {
            if (string.IsNullOrWhiteSpace(hparam))
                hparam = null;

            ReportGenerator reportGenerator = new ReportGenerator();
            var request = _reportService.GetHttpRequestMessage(ReportType.REPORT, hparam);
            var jsonString = await _reportService.ExecuteGet(request);
            var report = reportGenerator.GetRegionReport(jsonString).ToList();
            string result = "";
            string contentType = "";
            string fileName = "";
            if (submitButton.Equals("XML", StringComparison.OrdinalIgnoreCase))
            {
                contentType = "application/xml";
                fileName = "Report.xml";
                result = reportGenerator.GetXMLReport(report);
            }
            else if (submitButton.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                contentType = "text/csv";
                fileName = "Report.csv";
                result = reportGenerator.GetCSVReport(report);
            }
            else
            {
                contentType = "application/json";
                fileName = "Report.json";
                result = reportGenerator.GetJsonReport(report);
            }
            return File(Encoding.UTF8.GetBytes(result), contentType, fileName);
        }

    }
}