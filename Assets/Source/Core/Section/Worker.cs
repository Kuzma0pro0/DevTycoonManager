using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevIdle.Core
{
    public enum SkillType
    {
        Technology,
        Design,
        Speed
    }

    public enum WorkerGender
    {
        Man,
        Girl,
        Other
    }

    [Flags]
    public enum BallType
    {
        Technology = 1,
        Design = 2,
        Bug = 4
    }

    public class CreateBallsInfo
    {
        public double TimeDelayGeneratingBall;

        public bool ChanceBugBall;
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Worker
    {
        [JsonProperty]
        public int idAvatar;

        [JsonProperty]
        public WorkerGender Gender;

        [JsonProperty]
        public float Energy = 100;
        [JsonProperty]
        public float MaxEnergy = 100;

        public delegate void TechnologyBall();
        public delegate void DesignBall();
        public delegate void BugBall();
        public event TechnologyBall OnTechnologyball;
        public event DesignBall OnDesignball;
        public event BugBall OnBugBall;

        public delegate void Refresh();
        public event Refresh OnRefresh;

        public Worker()
        {
            Generate();
        }

        public void Generate()
        {

        }

        public bool IsLowEnergy()
        {
            return Energy >= 0 &&
                    Energy <= 25;
        }

        private double nextCreateTechnologyBall;
        private double nextCreateDesignBall;
        private double nextCreateBugBall;

        public (int Technology, int Design, int Bug) CreateBall(double time, BallType visualType, CreateBallsInfo info)
        {
            int technologyBall = 0;
            int designBall = 0;
            int bugBall = 0;

            bool visualTechnology = visualType.HasFlag(BallType.Technology);
            bool visualDesign = visualType.HasFlag(BallType.Design);
            bool visualBug = visualType.HasFlag(BallType.Bug);

            if (time >= nextCreateTechnologyBall)
            {
                float inaccuracy = (float)info.TimeDelayGeneratingBall * 0.1f;
                nextCreateTechnologyBall = time + info.TimeDelayGeneratingBall + UnityEngine.Random.Range(-inaccuracy, inaccuracy);
                if (visualTechnology)
                {
                    OnTechnologyball?.Invoke();
                    technologyBall++;
                }

                if (info.ChanceBugBall)
                {
                    if (visualBug)
                    {
                        OnBugBall?.Invoke();
                        bugBall++;
                    }
                }
            }

            if (time >= nextCreateDesignBall)
            {
                float inaccuracy = (float)info.TimeDelayGeneratingBall * 0.1f;
                nextCreateDesignBall = time + info.TimeDelayGeneratingBall + UnityEngine.Random.Range(-inaccuracy, inaccuracy);
                if (visualDesign)
                {
                    OnDesignball?.Invoke();
                    designBall++;
                }

                if (info.ChanceBugBall)
                {
                    if (visualBug)
                    {
                        OnBugBall?.Invoke();
                        bugBall++;
                    }
                }
            }

            if (bugBall > 0)
            {
                bugBall = 1;
            }

            return (technologyBall, designBall, bugBall);
        }

        public void Init()
        { }
    }
}
