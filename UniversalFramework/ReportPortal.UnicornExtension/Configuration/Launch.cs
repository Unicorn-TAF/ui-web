﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReportPortal.UnicornExtension.Configuration
{
    public class Launch
    {
        public string Name { get; set; }

        [JsonProperty("debugMode")]
        public bool IsDebugMode { get; set; }

        public List<string> Tags { get; set; }

        public string Description { get; set; }
    }
}
