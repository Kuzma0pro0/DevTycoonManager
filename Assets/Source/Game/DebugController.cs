using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DevIdle.Core;

namespace DevIdle
{
    public class DebugController : MonoBehaviour
    {
        public static bool DebugEnabled
        {
            get { return App.Environment == Environment.QA || App.Environment == Environment.Debug; }
        }

        public static bool ShowDebug
        {
            get
            {
                return DebugEnabled && showDebug;
            }
            set
            {
                showDebug = value;
            }
        }

        private static bool showDebug = true;

        private Text debugTextContainer;
        private float offset;
        private Dictionary<KeyCode, float> elapsedSincePressed = new Dictionary<KeyCode, float>();
        private float holdThreshold = 0.64f;
        private List<KeyCode> trackKeys = new List<KeyCode>()
        {
            KeyCode.F1
        };

        private float fpsMeasurePeriod = 0.5f;
        private int fpsAccumulator = 0;
        private float fpsNextPeriod = 0;
        private int currentFps;
        private string display = "{0} FPS";

        private void Awake()
        {
            if (!DebugEnabled)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        }

        private void Update()
        {
            foreach (var key in trackKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    elapsedSincePressed.Add(key, 0);
                }

                if (Input.GetKeyUp(key))
                {
                    if (elapsedSincePressed.ContainsKey(key))
                    {
                        elapsedSincePressed.Remove(key);
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.F5))
            {
                App.SaveProfile();
            }

            if (Input.GetKeyUp(KeyCode.F1))
            {
                App.Profile.Player.Studio.AddMoney(10000);
            }

            if (IsHoldingKey(KeyCode.F1))
            {
                var seconds = (int)elapsedSincePressed[KeyCode.F1];
                var value = Math.Pow(10, seconds + 3);

                App.Profile.Player.Studio.AddMoney(value);
            }

            var keys = new List<KeyCode>();
            foreach (var key in elapsedSincePressed.Keys)
            { keys.Add(key); }
            foreach (var key in keys)
            {
                elapsedSincePressed[key] += Time.deltaTime;
            }

            FPSCounterUpdate();
        }

        private void FPSCounterUpdate()
        {
            fpsAccumulator++;
            if (Time.realtimeSinceStartup > fpsNextPeriod)
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
            }
        }

        private bool IsHoldingKey(KeyCode code)
        {
            return elapsedSincePressed.ContainsKey(code) && elapsedSincePressed[code] > holdThreshold;
        }

        private void OnGUI()
        {
            if (ShowDebug)
            {
                DrawDebugInfo();
            }

            if (debugTextContainer != null)
            {
                debugTextContainer.enabled = ShowDebug;
            }
        }       

        private bool disableDebugTextContainer = false;
        private void DrawDebugInfo()
        {
            if (!disableDebugTextContainer && (debugTextContainer == null || debugTextContainer.IsDestroyed()))
            {
                debugTextContainer = GameObject.Find("DebugText")?.GetComponent<Text>();
                disableDebugTextContainer = true;
            }

            var hotkeys =
                $"save: <b>F5</b> :: cash: <b>F1</b>";
            var info =
                $"time: {new GameTime(App.Profile.Player.Timeline.CurrentTime).ToString("f")}\n" +
                $"theme: {App.Profile.Player.currentStudioTheme}\n";
            var fps =
                string.Format(display, currentFps);

            if (debugTextContainer == null)
            {
                DrawText(fps, new GUIStyle(GUI.skin.label) { fontSize = 45 });
                DrawText(hotkeys, new GUIStyle(GUI.skin.label) { fontSize = 30 });
                DrawText(info, new GUIStyle(GUI.skin.label) { fontSize = 30 });

                FlushText();
            }
            else
            {
                debugTextContainer.text = String.Join("\n", hotkeys, fps, info);
            }
        }

        private void DrawText(string text, GUIStyle style = null, Color? color = null)
        {
            const float topOffset = 320;
            const float gapBetweenLines = 10;

            style = style ?? GUI.skin.label;

            var size = style.CalcSize(new GUIContent(text));
            var position = new Vector2(gapBetweenLines, offset + topOffset);
            var rect = new Rect(position, size);

            GUI.color = color ?? Color.black;
            GUI.Label(rect, text, style);

            offset += size.y;
        }

        private void FlushText()
        {
            offset = 0;
        }

        private void OnDestroy()
        { }
    }
}