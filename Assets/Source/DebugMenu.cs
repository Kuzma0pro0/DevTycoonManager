using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace DevIdle
{
#if UNITY_EDITOR
    public static class DebugMenu
    {
        [MenuItem("Idle DevTycoon/Profile/Reset")]
        public static void ResetProfile()
        {
            Directory.Delete(App.GetProfileDirectory(), true);
        }

        [MenuItem("Idle DevTycoon/Profile/Reset", validate = true)]
        public static bool ResetProfileValidate()
        {
            return !Application.isPlaying;
        }

        [MenuItem("Idle DevTycoon/Test/Clear Bank")]
        public static void Test()
        {
            App.Profile.Player.Studio.Sections[0].ClearBallsBank(App.Profile.Player.Studio);
        }
    }
#endif
}
