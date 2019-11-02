using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using DevIdle.Game.Place;

namespace DevIdle.Core
{
    public enum SectionType
    {
        Manager,
        Programmer,
        Artist,
        Tester
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Section
    {
        [JsonProperty]
        public List<Worker> WorkerList = new List<Worker>();

        [JsonProperty]
        public SectionType Type;

        public delegate void Refresh();
        public event Refresh OnRefresh;

        public void Update(float delta)
        {

        }
    }
}