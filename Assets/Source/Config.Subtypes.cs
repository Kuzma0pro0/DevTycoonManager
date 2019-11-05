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

            [JsonProperty("multiplier")]
            public double Multiplier;
        }

        [JsonObject]
        public class SectionConfig
        {
            [JsonProperty("max_bank_balls")]
            public double MaxBankBalls = 300;

            [JsonProperty("base_upgrade_price")]
            public double BaseUpgradePrice = 0;

            [JsonProperty("upgrade_price_grow_multiplier")]
            public double UpgradePriceGrowMultiplier;

            [JsonProperty("base_production_value")]
            public double BaseProdiction;

            [JsonProperty("production_grow_multiplier")]
            public double ProductionGrowMultiplier;

            [JsonProperty("speed")]
            public float BaseSpeed = 1;

            [JsonProperty("price")]
            public double Price = 0;

            [JsonProperty("max_level")]
            public int MaxLevel = 1500;

            [JsonProperty("salary_prices")]
            public double[] SalaryPrices;

            [JsonProperty("speed_percentage_increase_per_salary_level")]
            public float SpeedIncreasePerSalaryLevel = 0.05f;

            [JsonProperty("max_salary_level")]
            public int MaxSalaryLevel = 100;

            [JsonProperty("max_workers_count")]
            public int MaxWorkersCount = 5;

            [JsonProperty("worker_hire_prices")]
            public double[] WorkerHirePrices = new double[1] { 0 };

            [JsonProperty("stages")]
            public StageConfig[] Stages = new StageConfig[0];
        }

    }
}
