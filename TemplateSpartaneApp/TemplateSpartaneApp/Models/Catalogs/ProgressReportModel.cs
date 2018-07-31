using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TemplateSpartaneApp.Abstractions;

namespace TemplateSpartaneApp.Models.Catalogs
{
    public partial class ProgressReportList : ModelBase
    {
        [JsonProperty("Progress_Reports")]
        public List<ProgressReportModel> ProgressReports { get; set; }

        [JsonProperty("RowCount")]
        public int RowCount { get; set; }
    }

    public partial class ProgressReportModel : ModelBase
    {
        [JsonProperty("ReportId")]
        public int ReportId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
