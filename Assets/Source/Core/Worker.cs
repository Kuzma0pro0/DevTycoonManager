using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevIdle.Core
{
    public enum WorkerType
    {
        Neutral,
        Programmer,
        GameDesigner,
        Artist,
        Tester,
        OfficeManager
    }

    public enum WorkerGender
    {
        Man,
        Girl,
        Other
    }

    public enum WorkerRarity
    {
        Common = 0,
        Special = 1,
        Gold = 2,
        Elite = 3
    }

    public enum SkillType
    {
        Technology,
        Design,
        Speed
    }

    [Flags]
    public enum BallType
    {
        Technology = 1,
        Design = 2,
        Bug = 4
    }

    public enum WorkType
    {
        Freelance,
        CreateGame
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Worker
    {
        [JsonProperty]
        public WorkerType Type;
        [JsonProperty]
        public WorkerGender Gender;
        [JsonProperty]
        public WorkerRarity Rarity;

        [JsonProperty]
        public int idFirstName;
        [JsonProperty]
        public int idLastName;
        [JsonProperty]
        public int idAvatar;
        [JsonProperty]
        public int IdInformation;

        [JsonProperty]
        public double Salary;

        [JsonProperty]
        private int technologySkill = 10;
        [JsonProperty]
        private int designSkill = 10;
        [JsonProperty]
        private int speedSkill = 10;

        [JsonProperty]
        public float Energy = 100;
        [JsonProperty]
        public float MaxEnergy = 100;

        [JsonProperty]
        public int Level
        { get; set; } = 0;
        [JsonProperty]
        public int LevelScore
        { get; set; } = 0;

        public bool IsLowEnergy()
        {
            return Energy >= 0 &&
                    Energy <= 25;
        }

        public int GetSkill(SkillType type, bool useBoost = true)
        {

            int value = 0;

            switch (type)
            {
                case SkillType.Technology:
                    {
                        value = technologySkill;
                        break;
                    }
                case SkillType.Design:
                    {
                        value = designSkill;
                        break;
                    }
                case SkillType.Speed:
                    {
                        value = speedSkill;
                        break;
                    }
            }

            if (useBoost)
            {
                if (IsLowEnergy())
                {
                    value = (int)(value * 0.75f);
                }

                return value;
            }
            else
            {
                return value;
            }
        }

        private double nextCreateTechnologyBall;
        private double nextCreateDesignBall;
        private double nextCreateBugBall;

        public double GetTimeDelayGeneratingTechnologyBall()
        {
            return 1;
        }
        public double GetTimeDelayGeneratingDesignBall()
        {
            return 1;
        }

        public bool ChanceCreatingBall(WorkType type, int internalStage = 0)
        {
            bool chance = false;

            switch (type)
            {
                case WorkType.Freelance:
                    {
                        chance = UnityEngine.Random.Range(0, 100f) > 25f;
                        break;
                    }
                case WorkType.CreateGame:
                    {
                        //chance = UnityEngine.Random.Range(0, 100f) < Config.Instance.CreateConfigs.GameRelationTechnology[stage][internalStage];
                        break;
                    }
            }
            return chance;
        }
        public bool ChanceBugBall()
        {
            var chance = (UnityEngine.Random.Range(0, 100f) < 30) ? true : false;
            return chance;
        }

        public (int Technology, int Design) CreateBall(double time, BallType visualType)
        {
            int TechnologyBall = 0;
            int DesignBall = 0;

            bool visualTechnology = visualType.HasFlag(BallType.Technology);
            bool visualDesign = visualType.HasFlag(BallType.Design);

            if (time >= nextCreateTechnologyBall)
            {
                nextCreateTechnologyBall = time + GetTimeDelayGeneratingTechnologyBall() + UnityEngine.Random.Range(-0.1f, 0.11f);
                if (ChanceCreatingBall(WorkType.Freelance))
                {
                    if (visualTechnology)
                    {
                        OnTechnologyball?.Invoke();
                        TechnologyBall++;
                    }
                }
            }

            if (time >= nextCreateDesignBall)
            {
                nextCreateDesignBall = time + GetTimeDelayGeneratingDesignBall() + UnityEngine.Random.Range(-0.1f, 0.11f);
                if (ChanceCreatingBall(WorkType.Freelance))
                {
                    if (visualDesign)
                    {
                        OnDesignball?.Invoke();
                        DesignBall++;
                    }
                }
            }

            return (TechnologyBall, DesignBall);
        }
        public int DestroyBugBall(double time)
        {
            int BugBall = 0;

            if (time >= nextCreateBugBall)
            {
                nextCreateBugBall = time + GetTimeDelayGeneratingTechnologyBall() + UnityEngine.Random.Range(0.5f, 1.5f);
                if (ChanceBugBall())
                {                    
                    OnBugBall?.Invoke();
                    BugBall++;
                }
            }

            return BugBall;
        }

        public delegate void TechnologyBall();
        public delegate void DesignBall();
        public delegate void BugBall();
        public event TechnologyBall OnTechnologyball;
        public event DesignBall OnDesignball;
        public event BugBall OnBugBall;

        public void Init()
        { }
    }
}
