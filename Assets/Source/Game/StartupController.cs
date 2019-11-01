using System.IO;
using UnityEngine;

namespace DevIdle.Game
{
    public class StartupController : MonoBehaviour
    {
        public bool DoNotCreateNewProfile;

        private static bool initialized;

        private static void Initialize(bool allowCreatingProfile = false)
        {
            if (initialized)
            {
                return;
            }

            Application.targetFrameRate = 60;
            App.LoadSettings();

            if (App.Profile == null)
            {
                try
                {
                    App.LoadProfile("profile");
                }
                catch (FileNotFoundException)
                {
                    if (allowCreatingProfile)
                    {
                        App.CreateNewProfile("profile");
                        App.SaveProfile();
                    }
                }
            }

            Config.Reload();
            Application.quitting += QuittingHandler;

            initialized = true;
        }

        private static void QuittingHandler()
        {
            if (App.Profile != null)
            {
                App.SaveProfile();
            }
        }

        private void Awake()
        {
            Initialize(!DoNotCreateNewProfile);
            Destroy(gameObject);
        }
    }
}
