using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;
using DevIdle.Game.UI;

namespace DevIdle.Game.Place
{
    public class OpenSpacePlaceController : MonoBehaviour
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
        private Section currentSection;

        public List<WorkerPlaceController> Workers = new List<WorkerPlaceController>();

        public void Init()
        {
            currentSection.OnRefresh += Refresh;

            for (int i = 0; i < currentSection.WorkerList.Count; i++) 
            {
                var worker = currentSection.WorkerList[i];
                var place = Workers[i];

                place.CurrentWorker = worker;
            }
        }

        public void Refresh()
        {

        }

        public void OpenScreen()
        {
            FindObjectOfType<UIController>().OpenScreen(ScreenType.Section, CurrentSection);
        }

        private void OnDestroy()
        {
            if (currentSection != null)
            {
                currentSection.OnRefresh -= Refresh;
            }
        }
    }
}