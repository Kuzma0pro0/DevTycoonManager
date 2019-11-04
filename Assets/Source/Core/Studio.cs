using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevIdle.Core
{
    public enum StudioTheme
    {
        Khabarovsk
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Studio
    {
        public double Capital
        { get { return capital; } }

        public double Technology
        { get { return technology; } }

        public double Design
        { get { return design; } }

        public StudioTheme Theme
        { get { return theme; } }

        [JsonProperty]
        private StudioTheme theme = StudioTheme.Khabarovsk;

        [JsonProperty]
        public List<Section> Sections = new List<Section>();

        [JsonProperty]
        private double capital = 0;

        [JsonProperty]
        private double technology = 0;

        [JsonProperty]
        private double design = 0;

        public delegate void Refresh();
        public event Refresh OnRefresh;

        public Studio()
            : this(false)
        { }

        public Studio(bool createNew, StudioTheme theme = StudioTheme.Khabarovsk)
        {
            if (createNew)
            {

            }
        }

        public List<Worker> GetWorkers()
        {
            var workerList = new List<Worker>();

            foreach (var section in Sections)
            {
                foreach (var worker in section.workerList)
                {
                    workerList.Add(worker);
                }
            }

            return workerList;
        }

        public void Init()
        { }

        public void Update(float delta, double time)
        {
            foreach (var section in Sections)
            {
                section.Update(time);
            }
        }

        public void AddTechnology(double balls)
        {
            technology += balls;

#if DEBUG
            if (double.IsNaN(technology))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

        public void AddDesign(double balls)
        {
            design += balls;

#if DEBUG
            if (double.IsNaN(design))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

        public bool EnsureCapital(double amount)
        {
            return capital >= amount;
        }

        public void AddMoney(double amount)
        {
            capital += amount;

#if DEBUG
            if (double.IsNaN(capital))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

        public bool TrySpendMoney(double amount)
        {
            if (EnsureCapital(amount))
            {
                SpendMoney(amount);
                return true;
            }

            return false;
        }

        private void SpendMoney(double amount)
        {
            capital -= amount;

#if DEBUG
            if (double.IsNaN(capital))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }
    }
}
