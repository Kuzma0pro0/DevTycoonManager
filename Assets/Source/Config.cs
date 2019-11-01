using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace DevIdle
{
    public partial class Config
    {
        private static Config instance;
        private static object loadingLock = new object();

        private Config()
        { }

        public static Config Instance
        {
            get
            {
                lock (loadingLock)
                {
                    if (instance == null)
                    {
                        instance = new Config();
                        instance.Load();
                    }

                    return instance;
                }
            }
        }

        #region Variables



        #endregion

        public static void Reload()
        {
            var config = new Config();
            config.Load();

            lock (loadingLock)
            {
                instance = config;
            }
        }

        private void Load()
        {
            var deserializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include
            };

            var configAsset = Resources.Load<TextAsset>("Config/config");
            if (configAsset?.text != null)
            {
                JsonConvert.PopulateObject(configAsset.text, this, deserializerSettings);
            }

            EnsureConfig();
        }

        private void EnsureConfig()
        {

        }
    }
}
