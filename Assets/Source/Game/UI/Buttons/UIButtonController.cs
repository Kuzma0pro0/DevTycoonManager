using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DevIdle.Game.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class UIButtonController : MonoBehaviour
    {
            public ScreenType ScreenType;

            private void Awake()
            {
                GetComponent<Button>().onClick.AddListener(Clicked);
            }

            private void Destroy()
            {
                GetComponent<Button>().onClick.RemoveListener(Clicked);
            }

            private void Clicked()
            {
                FindObjectOfType<UIController>().OpenScreen(ScreenType);
            }
    }
}