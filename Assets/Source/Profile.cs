using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using DevIdle.Core;

namespace DevIdle
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Profile
    {
        public static Profile Create()
        {
            return new Profile(true);
        }

        [JsonProperty]
        public DateTime CreationTime
        { get; private set; } = DateTime.UtcNow;

        [JsonProperty]
        public DateTime LastSaveTime
        { get; set; }

        [JsonProperty]
        public Other Other
        { get; set; } = new Other();

        [JsonProperty]
        public Studio Studio
        { get; set; } = new Studio();

        public Profile()
            : this(false)
        { }

        private Profile(bool initAsNew)
        {
            if (initAsNew)
            {
                
            }
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Other
    {
        [JsonObject]
        public class IAP
        {

        }

        public Other()
        { }

        [JsonProperty]
        public IAP IAPSStatistics
        { set; get; }
    }
}
