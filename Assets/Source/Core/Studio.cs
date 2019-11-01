using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevIdle.Core
{
    [Flags]
    public enum TimelineState
    {
        None = 0,
        Pause = 1
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Studio
    {
        public Timeline Timeline
        { get { return timeline; } }

        [JsonProperty]
        private Timeline timeline = new Timeline();

        private TimelineState pauseFlag;

        public Studio()
            : this(false)
        { }

        public Studio(bool createNew)
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
        }
    }
}
