using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevIdle.Core
{
    public enum Publisher
    {
        Minisoft,
        Value
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Studio
    {
        public double Capital
        { get { return capital; } }

        [JsonProperty]
        public List<Worker> WorkerList = new List<Worker>();

        [JsonProperty]
        public List<Section> Sections = new List<Section>();

        [JsonProperty]
        public float MainBestScore = 32;

        [JsonProperty]
        public float SecondaryBestScore = 0;

        [JsonProperty]
        private double capital = 0;

        public Studio()
            : this(false)
        { }

        public Studio(bool createNew)
        {
            if (createNew)
            {

            }
        }

        public List<Worker> GetWorkers(bool all = true, WorkerType type = WorkerType.Neutral)
        {
            var workerList = new List<Worker>();

            foreach (var section in Sections)
            {
                foreach (var worker in section.WorkerList)
                {
                    workerList.Add(worker);
                }
            }
            foreach (var worker in WorkerList)
            {
                workerList.Add(worker);
            }

            if (!all)
            {
                workerList = (List<Worker>)workerList.Where((x) => x.Type == type);
            }

            return workerList;
        }

        public void Init()
        { }

        public void Update(float delta)
        {
            foreach (var section in Sections)
            {
                section.Update(delta);
            }
        }
    }
}
