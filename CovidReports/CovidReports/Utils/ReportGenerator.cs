using CovidReports.Models;
using CovidReports.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace CovidReports.Utils
{
    public class ReportGenerator
    {
        public Report<Region> GetRegions(string jsonString)
        {
            var regions = JsonConvert.DeserializeObject<Report<Region>>(jsonString);
            regions.Data = regions.Data.OrderBy(i => i.Iso);
            return regions;
        }
        public IEnumerable<RegionReportViewModel> GetRegionReport(string jsonString)
        {
            var regions = JsonConvert.DeserializeObject<Report<Datum>>(jsonString);
            var groupByRegionResult = regions.Data.GroupBy(r => r.Region.Name);
            List<RegionReportViewModel> regionReportList = new List<RegionReportViewModel>();
            RegionReportViewModel regionReport = null;
            foreach (var region in groupByRegionResult)
            {
                regionReport = new RegionReportViewModel();
                regionReport.Name = region.Key;
                regionReport.Confirmed = region.Sum(c => c.Confirmed);
                regionReport.Deadths = region.Sum(d => d.Deaths);
                regionReportList.Add(regionReport);
            }
            var result = regionReportList.OrderByDescending(r => r.Confirmed).Take(10);
            return result;
        }

        public IEnumerable<RegionReportViewModel> GetProvinceReport(string jsonString)
        {
            var regions = JsonConvert.DeserializeObject<Report<Datum>>(jsonString);
            var orderedRegionResult = regions.Data.OrderByDescending(r => r.Confirmed).Take(10);
            List<RegionReportViewModel> regionReportList = new List<RegionReportViewModel>();
            RegionReportViewModel regionReport = null;
            foreach (var region in orderedRegionResult)
            {
                regionReport = new RegionReportViewModel();
                regionReport.Name = string.IsNullOrWhiteSpace(region.Region.Province) ? region.Region.Name : region.Region.Province;
                regionReport.Confirmed = region.Confirmed;
                regionReport.Deadths = region.Deaths;
                regionReportList.Add(regionReport);
            }
            return regionReportList;
        }

        public string GetXMLReport(List<RegionReportViewModel> report)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<RegionReportViewModel>));
            StreamWriter streamWriter = null;
            MemoryStream memoryStream = new MemoryStream();
            streamWriter = new StreamWriter(memoryStream);

            serialiser.Serialize(streamWriter, report);

            var buffer = Encoding.ASCII.GetString(memoryStream.GetBuffer());
            return buffer;
        }

        public string GetJsonReport(List<RegionReportViewModel> report)
        {
            return JsonConvert.SerializeObject(report);
        }

        public string GetCSVReport(List<RegionReportViewModel> report)
        {
            var sb = new StringBuilder();
            foreach (var data in report)
            {
                sb.AppendLine(data.Name + "," + data.Confirmed + "," + data.Deadths);
            }
            return sb.ToString();
        }
    }
}