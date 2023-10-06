using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace complainSystem.models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ComplainStatus
    {


        Open = 1,
        Closed = 2,
        Pending = 3,



    }
}