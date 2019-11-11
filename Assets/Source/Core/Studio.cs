using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

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

        [JsonProperty]
        public Section OpenSpace = new Section();

        [JsonProperty]
        public List<Section> Sections = new List<Section>();

        [JsonProperty]
        private double capital = 0;

        [JsonProperty]
        private long technology = 0;

        [JsonProperty]
        private long design = 0;

        public delegate void Refresh();
        public event Refresh OnRefresh;

        public Studio()
        { }

        public List<Worker> GetWorkers()
        {
            var workerList = new List<Worker>();

            foreach (var section in Sections)
            {
                foreach (var worker in section.WorkerList)
                {
                    workerList.Add(worker);
                }
            }

            return workerList;
        }

        public void Init()
        { }

        public bool TryUpdateSection(Section section)
        {
            if (!Sections.Contains(section))
            {
                Debug.Log("Section not set");
                return false;
            }

            var price = section.CalcUpgradePrice();
            if (!EnsureCapital(price))
            {
                return false;
            }

            if (!section.TryUpgrade())
            {
                return false;
            }

            SpendMoney(price);
            return true;
        }

        public void Update(float delta, double time)
        {
            OpenSpace.Update(time);
                
            foreach (var section in Sections)
            {
                section.Update(time);
            }
        }

        public void AddTechnology(long balls)
        {
            technology += balls;

#if DEBUG
            if (double.IsNaN(technology))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

        public void AddDesign(long balls)
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
