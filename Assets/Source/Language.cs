using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DevIdle
{
    public partial class Language
    {
        private static Language _instance = null;

        public static Language Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Language();
                }
                return _instance;
            }
        }

        private string currentLanguage = "ru_RU";

        private Language()
        {
            //if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            //{
            //    currentLanguage = "ru_RU";
            //}
            //else
            //{
            //    currentLanguage = "en_US";
            //}

            Load();

            Debug.Log("Language " + currentLanguage);
        }

        private string json;
        public LanguageDictionary Dictionary = new LanguageDictionary();

        public string Get(string key)
        {
            var result = GetPrivate(key);
            if (result == null)
            {
                Debug.LogError("Failed to found message with key " + key);
                return $"{key}_NOTFOUND";
            }

            return result;
        }
        public string Get(string key, object p0)
        {
            return String.Format(Get(key), p0);
        }
        public string Get(string key, object p0, object p1)
        {
            return String.Format(Get(key), p0, p1);
        }
        public string Get(string key, object p0, object p1, object p2)
        {
            return String.Format(Get(key), p0, p1, p2);
        }

        private string GetPrivate(string key)
        {
            var plural = false;
            var _key = key.Split('$');
            if (_key.Length > 1)
            {
                plural = _key[1].StartsWith("p");
            }

            if (Dictionary.Dict.TryGetValue(_key[0], out var result))
            {
                var _result = result as JObject;
                if (_result != null && _result.Type == JTokenType.Object)
                {
                    JToken r;

                    if (plural && _result.TryGetValue("plural", out r))
                    {
                        return r.Value<string>();
                    }
                    else if (_result.TryGetValue("singular", out r))
                    {
                        return r.Value<string>();
                    }
                }
                else
                {
                    return (string)result;
                }

            }

            return default;
        }

        public void Load()
        {
            var path = Application.streamingAssetsPath + "/Language/" + currentLanguage + ".json";

            LanguageDictionary loadedDictionary = null;
            try
            {
                loadedDictionary = DeserializeProfile(path);
            }
            catch (Exception e0)
            {
                throw new Exception("Failed to deserialize LanguageDictionary. " + e0);
            }

            Dictionary = loadedDictionary;
        }
        private LanguageDictionary DeserializeProfile(string path)
        {
#if UNITY_ANDROID
            var request = new WWW(path);
            while (!request.isDone)
            {
                continue;
            }

            using (var stream = new MemoryStream(request.bytes))
#else
            using (var stream = File.OpenRead(path))
#endif
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                return JsonSerializer.Create(settings).Deserialize<LanguageDictionary>(jsonReader);
            }
        }

        [Serializable]
        public class LanguageDictionary
        {
            public Dictionary<string, object> Dict = new Dictionary<string, object>();
        }
    }
}
