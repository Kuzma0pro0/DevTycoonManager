using System.IO;

namespace DevIdle
{
    public enum Environment
    {
        QA,
        Production,
        Debug
    }

    public partial class App
    {
        public static Environment Environment
        {
            get
            {
                return Environment.QA;
            }
        }
        
        public static string GetAppDirectory()
        {
#if !UNITY_EDITOR
            return Path.Combine(
                Application.persistentDataPath,
                "BoomDrag Games",
                "DevTycoon Manager"
                );
#else
            return
                Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
                    "BoomDrag Games",
                    "DevTycoon Manager"
                );
#endif
        }

        public static string GetProfileDirectory()
        {
            return
                Path.Combine(
                    GetAppDirectory(),
                    "Profile"
                );
        }
    }
}
