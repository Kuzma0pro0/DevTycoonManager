using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

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

        public List<SectionStagePrefabInfo> Stages = new List<SectionStagePrefabInfo>();

        public void Init()
        {
            currentSection.OnRefresh += Refresh;
        }

        public void Refresh()
        {

        }

        private void OnDestroy()
        {
            currentSection.OnRefresh -= Refresh;
        }
    }
}