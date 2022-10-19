﻿using FTLSupporter;
using Newtonsoft.Json;

namespace FTLApi.DTO.Request
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class FTLSupportRequest
    {
        [JsonProperty("taskList")]
        public List<FTLTask> TaskList { get; set; }

        [JsonProperty("truckList")]
        public List<FTLTruck> TruckList { get; set; }
    }
}