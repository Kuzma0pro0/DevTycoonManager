using Newtonsoft.Json;
using System.Collections.Generic;
using DevIdle.Core;

namespace DevIdle
{
    public partial class Config
    {
        [JsonObject]
        public class StudioPreset
        {
            public Dictionary<Publisher, List<Studio>> Preset = new Dictionary<Publisher, List<Studio>>()
            {
                {Publisher.Minisoft, new List<Studio>()
                    {
                        new Studio
                        {

                        },
                        new Studio
                        {

                        }
                    }
                }
            };
        }

    }
}
