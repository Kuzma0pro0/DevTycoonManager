using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game.Place
{
    public class SectionPlaceController : MonoBehaviour
    {
        public Section CurrentSection
        {
            get
            {
                return currentSection;
            }
            set
            {
                currentSection = value;

                Init();
            }
        }

        public Section currentSection;

        private Studio studio;

        private void Start()
        {
            studio = PlayerController.Instance.Player.Studio;
        }

        public void Init()
        {
            currentSection.OnRefresh += Refresh;
        }

        public void Refresh()
        {

        }
    }
}