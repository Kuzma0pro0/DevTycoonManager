using Newtonsoft.Json;
using System.Collections.Generic;
using DevIdle.Core;

namespace DevIdle
{
    public partial class Config
    {
        [JsonObject]
        public class ThemePreset
        {
            [JsonProperty("preset")]
            public Dictionary<StudioTheme, Studio> Preset = new Dictionary<StudioTheme, Studio>()
            {
                { StudioTheme.Khabarovsk,
                    new Studio
                    {
                        OpenSpace = new Section
                            {
                                Bought = true,
                                Type = SectionType.OpenSpace,
                                CurrentBallType = BallType.Bug | BallType.Design | BallType.Technology,
                                WorkerList = new List<Worker>
                                {
                                    new Worker()
                                }
                            }
                    }
                }
            };
        }

        [JsonObject]
        public class StageConfig
        {
            [JsonProperty("unlock_level")]
            public int UnlockLevel;
        }

        [JsonObject]
        public class SectionConfig
        {
            [JsonProperty("base_upgrade_price")]
            public double BaseUpgradePrice = 0;

            [JsonProperty("upgrade_price_grow_multiplier")]
            public double UpgradePriceGrowMultiplier;

            [JsonProperty("price")]
            public double Price = 0;

            [JsonProperty("max_level")]
            public int MaxLevel = 1500;

            [JsonProperty("upgrade_ball_grow_multiplier")]
            public double UpgradeBallGrowMultiplier = 1f;

            [JsonProperty("base_ball_per_minute")]
            public double BaseBallPerMinute = 10f;

            [JsonProperty("base_bank_balls")]
            public double BasePoolBalls = 300;

            [JsonProperty("upgrade_pool_balls_grow_multiplier")]
            public double UpgradePoolBallsGrowMultiplier;

            [JsonProperty("experience_prices")]
            public double[] ExperiencePrices;

            [JsonProperty("max_experience_level")]
            public int MaxExperienceLevel = 100;

            [JsonProperty("max_workers_count")]
            public int MaxWorkersCount = 5;

            [JsonProperty("worker_hire_prices")]
            public double[] WorkerHirePrices = new double[1] { 0 };

            [JsonProperty("stages")]
            public StageConfig[] Stages = new StageConfig[0];
        }
    }
}
