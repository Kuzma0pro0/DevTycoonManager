using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace DevIdle
{
    public static partial class App
    {
        public static bool AllowSaving = true;

        public static event Action ProfileLoaded;
        public static event Action ProfileUnloaded;

        public static Profile Profile
        { get; private set; }

        private static string currentProfileName;

        private static object opLock = new object();

        public static bool ProfileFileExists(string name)
        {
            var path = Path.Combine(GetProfileDirectory(), name);
            return File.Exists(path);
        }

        public static void ResetProfile()
        {
            if (AllowSaving)
            {
                var name = currentProfileName;
                currentProfileName = $"{name}.{Guid.NewGuid().ToString("N")}";
                SaveProfile();

                var directory = GetProfileDirectory();
                var path = Path.Combine(directory, name);
                var backupPath = $"{path}.backup";
                if (Directory.Exists(directory))
                {
                    File.Delete(path);
                    File.Delete(backupPath);
                }
            }

            currentProfileName = null;
            Profile = null;
            ProfileUnloaded?.Invoke();
        }

        public static void CreateNewProfile(string name = null)
        {
            Profile = Profile.Create();
            currentProfileName = name ?? Guid.NewGuid().ToString("N");
            ProfileLoaded?.Invoke();
        }

        public static byte[] LoadProfileBinary(string name)
        {
            var path = Path.Combine(GetProfileDirectory(), name);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Couldn't find profile at {path}.", path);
            }

            using (var stream = File.OpenRead(path))
            using (var reader = new BinaryReader(stream))
            using (var outstream = new MemoryStream())
            {
                stream.CopyTo(outstream);
                return outstream.ToArray();
            }
        }

        public static Profile LoadProfileData(string name)
        {
            var path = Path.Combine(GetProfileDirectory(), name);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Couldn't find profile at {path}.", path);
            }

            return DeserializeProfile(path);
        }

        public static void LoadProfile(string name)
        {
            var path = Path.Combine(GetProfileDirectory(), name);
            var backupPath = $"{path}.backup";
            if (!File.Exists(path) && !File.Exists(backupPath))
            {
                throw new FileNotFoundException($"Couldn't find profile at {path}.", path);
            }

            Profile loadedProfile = null;
            try
            {
                loadedProfile = DeserializeProfile(path);
            }
            catch (Exception e0)
            {
                if (File.Exists(backupPath))
                {
                    Debug.LogWarning($"Trying to load backup after failed to load profile {name}");
                    try
                    {
                        if (File.Exists(backupPath))
                        {
                            loadedProfile = DeserializeProfile(backupPath);
                        }
                    }
                    catch (Exception e1)
                    {
                        throw new Exception("Failed to deserialize profile and its backup. " + e1);
                    }
                }
                else
                {
                    throw new Exception("Failed to deserialize profile. " + e0);
                }
            }


            lock (opLock)
            {
                Profile = loadedProfile;
                currentProfileName = name;
            }

            ProfileLoaded?.Invoke();
        }

        public static void SaveProfileData(string name, byte[] data)
        {
            var directory = GetProfileDirectory();
            var path = Path.Combine(directory, name);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(data);
            }
        }

        public static void SaveProfile()
        {
            if (!AllowSaving)
            {
                return;
            }

            lock (opLock)
            {
                if (Profile == null)
                {
                    throw new InvalidOperationException("Profile not set.");
                }

                if (currentProfileName == null)
                {
                    throw new InvalidOperationException("Profile name not set.");
                }

                Profile.LastSaveTime = GlobalTime.Current;

                var directory = GetProfileDirectory();
                var path = Path.Combine(directory, currentProfileName);
                var newFilePath = $"{path}.new";
                var backupPath = $"{path}.backup";

                Directory.CreateDirectory(directory);
                if (File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }

                try
                {
                    using (var stream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (var writer = new StreamWriter(stream))
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        var settings = new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        };

                        JsonSerializer.Create(settings).Serialize(jsonWriter, Profile);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to serialize profile. " + e);
                }

                // TODO: Check if everything is alright with just saved profile

                if (File.Exists(path))
                {
                    try
                    {
                        if (File.Exists(backupPath))
                        {
                            File.Delete(backupPath);
                        }

                        File.Move(path, backupPath);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed to create profile backup." + e);
                    }
                }

                File.Move(newFilePath, path);
            }
        }

        public static void SaveProfile(string name, Profile profile)
        {
            var directory = GetProfileDirectory();
            var path = Path.Combine(directory, name);
            var newFilePath = $"{path}.new";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }

            try
            {
                using (var stream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new StreamWriter(stream))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    JsonSerializer.Create(settings).Serialize(jsonWriter, profile);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to serialize profile." + e);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.Move(newFilePath, path);
        }

        public static void ReplaceProfileData(string name, byte[] data)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            lock (opLock)
            {
                var directory = GetProfileDirectory();
                var path = Path.Combine(directory, currentProfileName);
                var backupPath = $"{path}.backup";

                if (File.Exists(path))
                {
                    try
                    {
                        if (File.Exists(backupPath))
                        {
                            File.Delete(backupPath);
                        }

                        File.Move(path, backupPath);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed to create profile backup. " + e);
                    }
                }

                File.WriteAllBytes(path, data);
            }
        }

        private static Profile DeserializeProfile(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                return JsonSerializer.Create(settings).Deserialize<Profile>(jsonReader);
            }
        }
    }
}
