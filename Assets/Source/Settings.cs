using Newtonsoft.Json;

namespace DevIdle
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Settings
    {
        [JsonProperty]
        public bool MusicEnabled = true;

        [JsonProperty]
        public bool SpundEnabled = true;
    }
}
