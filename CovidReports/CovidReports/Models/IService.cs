using CovidReports.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CovidReports.Models
{
    interface IService
    {
        Task<string> ExecuteGet(HttpRequestMessage httpMessage);
        HttpRequestMessage GetHttpRequestMessage(ReportType type, string region);
    }
}
