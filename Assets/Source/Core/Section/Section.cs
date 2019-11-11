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
        OpenSpace,
        Manager,
        Programmer,
        Artist,
        Tester
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Section
    {
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
                return WorkerList.Count >= config.MaxWorkersCount;
            }
        }

        public bool AtMaxSalaryLevel
        {
            get
            {
                return experienceLevel >= config.MaxExperienceLevel;
            }
        }

        public bool AtMaxBankBalls
        {
            get
            {
                return technologyBank + designBank + bugBank > CalcPoolBals();
            }
        }

        public int Level
        { get { return level; } }

        public int Workers
        { get { return WorkerList.Count; } }

        public int ExperienceLevel
        { get { return experienceLevel; } }

        public int Stage
        { get { return stage; } }

        public int MaxWorker
        { get { return config.MaxWorkersCount; } }

        [JsonProperty]
        public bool Bought;

        [JsonProperty]
        public List<Worker> WorkerList = new List<Worker>();

        [JsonProperty]
        public BallType CurrentBallType;

        [JsonProperty]
        public SectionType Type;

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

        private int stage = -1;

        public Section()
        { }

        public virtual double CalcUpgradePrice()
        {
            var result = config.BaseUpgradePrice * Math.Pow(1.0 + config.UpgradePriceGrowMultiplier, level - 1);
            return result;
        }

        public double CalcTimeDelayGeneratingBall()
        {
            var delay = 60f / (config.BaseBallPerMinute * (1.0f + config.UpgradeBallGrowMultiplier * (level - 1)));
            return delay;
        }

        public bool CalcChanceBugBall()
        {
            bool chance = false;

            chance = UnityEngine.Random.Range(0, 100f) < 30f;

            return chance;
        }

        public float CalcLevelProgress()
        {
            var (minLevel, maxLevel) = CalcStageLevelRange();

            if (minLevel == maxLevel)
            {
                return 1f;
            }

            return (level - minLevel) / (float)(maxLevel - minLevel);
        }

        public double CalcPoolBals()
        {
            var pool = config.BasePoolBalls + (experienceLevel * config.UpgradePoolBallsGrowMultiplier);
            return pool;
        }

        public (int min, int max) CalcStageLevelRange()
        {
            var minLevel = config.Stages[stage].UnlockLevel;
            var maxLevel = config.Stages.Length <= stage ? minLevel : config.Stages[stage + 1].UnlockLevel;

            return (minLevel, maxLevel);
        }

        public virtual bool TryUpgrade()
        {
            if (level >= config.MaxLevel)
            {
                return false;
            }

            level += 1;
            RefreshStage();
            return true;
        }

        public virtual bool TryUpgradeExperienceLevel()
        {            
            if (experienceLevel >= config.MaxExperienceLevel)
            {
                return false;
            }

            experienceLevel += 1;
            return true;
        }

        public virtual bool TryHireNextWorker()
        {
            if (WorkerList.Count >= config.MaxWorkersCount)
            {
                return false;
            }

            WorkerList.Add(new Worker());
            return true;
        }

        private void RefreshStage()
        {
            var newStage = -1;
            var stages = config.Stages;

            for (int i = 0; i < stages.Length; ++i)
            {
                if (stages[i].UnlockLevel > level)
                {
                    OnRefresh?.Invoke();
                    break;
                }

                newStage = i;
            }

            stage = newStage;
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
            if (AtMaxBankBalls)
            {
                return;
            }

            foreach (var worker in WorkerList)
            {
                var bank = worker.CreateBall(
                    time,
                    CurrentBallType,
                    new CreateBallsInfo
                    {
                        TimeDelayGeneratingBall = CalcTimeDelayGeneratingBall(),
                        ChanceBugBall = CalcChanceBugBall()
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