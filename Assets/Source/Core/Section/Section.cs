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
        public SectionType Type
        { get { return type; } }

        private Config.SectionConfig config
        { get { return Config.Instance.SectionConfigs[Type]; } }

        public bool AtMaxLevel
        {
            get
            {
                return level >= config.MaxLevel;
            }
        }

        public bool AllWorkersHired
        {
            get
            {
                return workerList.Count >= config.MaxWorkersCount;
            }
        }

        public bool AtMaxSalaryLevel
        {
            get
            {
                return experienceLevel >= config.MaxSalaryLevel;
            }
        }

        public bool AtMaxBankBalls
        {
            get
            {
                return technologyBank + designBank + bugBank > config.MaxBankBalls;
            }
        }

        public int Level
        { get { return level; } }

        public int Workers
        { get { return workerList.Count; } }

        public int ExperienceLevel
        { get { return experienceLevel; } }

        [JsonProperty]
        public List<Worker> workerList = new List<Worker>();

        [JsonProperty]
        protected SectionType type;

        [JsonProperty]
        protected int level = 1;

        [JsonProperty]
        protected int experienceLevel = 1;

        [JsonProperty]
        protected long technologyBank = 0;
        [JsonProperty]
        protected long designBank = 0;
        [JsonProperty]
        protected long bugBank = 0;

        public double GetTimeDelayGeneratingTechnologyBall()
        {
            return 1;
        }
        public double GetTimeDelayGeneratingDesignBall()
        {
            return 1;
        }

        public bool ChanceBugBall()
        {
            bool chance = false;

            chance = UnityEngine.Random.Range(0, 100f) < 30f;

            return chance;
        }

        public delegate void Refresh();
        public event Refresh OnRefresh;
        public delegate void Clear();
        public event Clear OnClear;

        public void ClearBallsBank(Studio studio)
        {
            if (bugBank != 0)
            {
                bugBank /= 2;
            }

            studio.AddTechnology(technologyBank - bugBank);
            studio.AddDesign(designBank - bugBank);

            technologyBank = 0;
            designBank = 0;
            bugBank = 0;

            OnClear?.Invoke();
        }

        public void Update(double time)
        {
            if (!AtMaxBankBalls)
            {
                return;
            }

            foreach (var worker in workerList)
            {
                var bank = worker.CreateBall(
                    time,
                    BallType.Bug | BallType.Design | BallType.Technology,
                    new CreateBallsInfo
                    {
                        TimeDelayGeneratingDesignBall = GetTimeDelayGeneratingDesignBall(),
                        TimeDelayGeneratingTechnologyBall = GetTimeDelayGeneratingTechnologyBall(),
                        ChanceBugBall = ChanceBugBall()
                    }
                    );

                if (!AtMaxBankBalls)
                {
                    technologyBank += bank.Technology;
                }
                if (!AtMaxBankBalls)
                {
                    designBank += bank.Design;
                }
                if (!AtMaxBankBalls)
                {
                    bugBank += bank.Bug;
                }                         
            }
        }
    }
}