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
    }
#endif
}
