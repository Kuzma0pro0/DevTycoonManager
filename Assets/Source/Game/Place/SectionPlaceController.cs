using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;
using DevIdle.Game.UI;

namespace DevIdle.Game.Place
{
    [Serializable]
    public class SectionStagePrefabInfo
    {
        [SerializeField]
        public int Stage;
        [SerializeField]
        public GameObject Prefab;
    }

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
        private Section currentSection;
        public Studio Studio;

        public List<SectionStagePrefabInfo> Stages = new List<SectionStagePrefabInfo>();

        public List<WorkerPlaceController> Workers = new List<WorkerPlaceController>();
            
        public void Init()
        {
            currentSection.OnRefresh += Refresh;
        }

        public void Refresh()
        {

        }

        public void Click()
        {
            if (CurrentSection.Bought)
            {
                OpenScreen();
            }
            else
            {
                OpenStudioScreen();
            }
        }

        private void OpenScreen()
        {
            FindObjectOfType<UIController>().OpenScreen(ScreenType.Section, CurrentSection);
        }

        private void OpenStudioScreen() 
        {
            FindObjectOfType<UIController>().OpenScreen(ScreenType.Studio);
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