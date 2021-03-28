using CovidReports.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CovidReports.Models
{
    public class ReportService : IService
    {
        private static HttpClient client = new HttpClient();
        private static readonly ReportService instance = new ReportService();

        static ReportService()
        {
        }

        private ReportService()
        {
        }

        public static ReportService Instance
        {
            get
            {
                return instance;
            }
        }
        public async Task<string> ExecuteGet(HttpRequestMessage httpMessage)
        {
            string body;
            using (var response = await client.SendAsync(httpMessage))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }
            return body;
        }

        public HttpRequestMessage GetHttpRequestMessage(ReportType type, string region)
        {
            string uri;
            string webAPIUrl = ConfigurationManager.AppSettings["WebAPIUrl"];
            string regionEndpoint = ConfigurationManager.AppSettings["Regions"];
            string reportsEndpoint = ConfigurationManager.AppSettings["Reports"];
            string keyHeader = ConfigurationManager.AppSettings["x-rapidapi-key"];
            string hostHeader = ConfigurationManager.AppSettings["x-rapidapi-host"];

            switch (type)
            {
                case ReportType.REGION:
                    uri = webAPIUrl + regionEndpoint;
                    break;
                case ReportType.REPORT:
                    if (region != null)
                    {
                        uri = webAPIUrl + reportsEndpoint + "?iso=" + region;
                    }
                    else
                    {
                        uri = webAPIUrl + reportsEndpoint;
                    }
                    break;
                default:
                    uri = null;
                    break;
            }

            if (uri == null)
                return null;

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers ={
                        { "x-rapidapi-key", keyHeader },
                        { "x-rapidapi-host", hostHeader },
                    },
            };

            return httpMessage;
        }
    }
}