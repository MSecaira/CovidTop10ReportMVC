using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidReports.Models
{
    public class Report<T>
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<T> Data { get; set; }
    }
    public class Datum
    {
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Date { get; set; }

        [JsonProperty("confirmed", NullValueHandling = NullValueHandling.Ignore)]
        public long? Confirmed { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("recovered", NullValueHandling = NullValueHandling.Ignore)]
        public long? Recovered { get; set; }

        [JsonProperty("confirmed_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? ConfirmedDiff { get; set; }

        [JsonProperty("deaths_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? DeathsDiff { get; set; }

        [JsonProperty("recovered_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? RecoveredDiff { get; set; }

        [JsonProperty("last_update", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public long? Active { get; set; }

        [JsonProperty("active_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? ActiveDiff { get; set; }

        [JsonProperty("fatality_rate", NullValueHandling = NullValueHandling.Ignore)]
        public double? FatalityRate { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public Region Region { get; set; }
    }

    public class Region
    {
        [JsonProperty("iso", NullValueHandling = NullValueHandling.Ignore)]
        public string Iso { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("province", NullValueHandling = NullValueHandling.Ignore)]
        public string Province { get; set; }

        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public string Lat { get; set; }

        [JsonProperty("long", NullValueHandling = NullValueHandling.Ignore)]
        public string Long { get; set; }

        [JsonProperty("cities", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<City> Cities { get; set; }
    }

    public class City
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Date { get; set; }

        [JsonProperty("fips")]
        public long? Fips { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("confirmed", NullValueHandling = NullValueHandling.Ignore)]
        public long? Confirmed { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("confirmed_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? ConfirmedDiff { get; set; }

        [JsonProperty("deaths_diff", NullValueHandling = NullValueHandling.Ignore)]
        public long? DeathsDiff { get; set; }

        [JsonProperty("last_update", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }
    }
}