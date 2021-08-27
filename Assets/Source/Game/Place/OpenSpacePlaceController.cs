using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;
using DevIdle.Game.UI;
using TMPro;

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
        public Studio Studio;

        public List<WorkerPlaceController> Workers = new List<WorkerPlaceController>();

        [Space]

        public TextMeshProUGUI Information;

        private string informationLabelTemplate;

        public void Init()
        {
            currentSection.OnRefresh += Refresh;
            currentSection.OnUpdateInformation += RefreshInformation;

            for (int i = 0; i < currentSection.WorkerList.Count; i++) 
            {
                var worker = currentSection.WorkerList[i];
                var place = Workers[i];

                place.CurrentWorker = worker;
            }

            Refresh();
        }

        public void Refresh()
        {
            RefreshInformation();
        }

        private void RefreshInformation() 
        {
            if (informationLabelTemplate == null)
            {
                informationLabelTemplate = Information.text;
            }

            var info = currentSection.GetBalls;

            Information.text = string.Format(informationLabelTemplate, info.technology, info.design, info.bug);
        }

        public void CollectBank() 
        {
            currentSection.ClearBallsBank(Studio);

            RefreshInformation();
        }

        private void OnDestroy()
        {
            if (currentSection != null)
            {
                currentSection.OnRefresh -= Refresh;
                currentSection.OnUpdateInformation -= RefreshInformation;
            }
        }
    }
}