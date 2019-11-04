using Newtonsoft.Json;
using System;

namespace DevIdle.Core
{
    [Flags]
    public enum TimelineState
    {
        None = 0,
        Pause = 1
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Player
    {
        public Timeline Timeline
        { get { return timeline; } }

        public Studio Studio
        { get { return studio; } }

        [JsonProperty]
        private Timeline timeline = new Timeline();

        [JsonProperty]
        private Studio studio = new Studio();

        private TimelineState pauseFlag;

        public Player()
            : this(false)
        { }

        public Player(bool createNew)
        {
            if (createNew)
            {

            }
        }

        public void Init()
        { }

        public void SetPause(TimelineState flag)
        {
            pauseFlag |= flag;
        }

        public void UnsetPause(TimelineState flag)
        {
            pauseFlag &= ~flag;
        }

        public void Update(float delta)
        {
            if (pauseFlag != TimelineState.None)
            {
                return;
            }

            timeline.Update(delta);

            var time = timeline.CurrentGameTime.Time;

            studio.Update(delta, time);
        }
    }
}