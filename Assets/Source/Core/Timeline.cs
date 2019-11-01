using Newtonsoft.Json;

namespace DevIdle.Core
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Timeline
    {
        public GameTime CurrentGameTime
        {
            get
            {
                return new GameTime(CurrentTime);
            }
        }

        [JsonProperty]
        public double CurrentTime
        { get; private set; } = 0;

        public void Set(double time)
        {
            CurrentTime = time;
        }

        public void Update(float delta)
        {
            CurrentTime += delta;
        }
    }
}