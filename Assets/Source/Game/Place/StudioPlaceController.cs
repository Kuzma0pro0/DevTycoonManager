using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game.Place
{
    [Serializable]
    public class SectionPrefabInfo
    {
        [SerializeField]
        public SectionType Type;
        [SerializeField]
        public SectionPlaceController Section;
    }

    public class StudioPlaceController : MonoBehaviour
    {
        private Studio studio;

        public OpenSpacePlaceController OpenSpace;

        public List<SectionPrefabInfo> SectionsPlace = new List<SectionPrefabInfo>();

        private void Start()
        {
            studio = PlayerController.Instance.Player.Studio;

            studio.OnRefresh += Refresh;

            Init();
        }

        private void Init()
        {
            OpenSpace.CurrentSection = studio.OpenSpace;
        }

        public void Refresh()
        {

        }

        private void OnDestroy()
        {
            studio.OnRefresh -= Refresh;
        }
    }
}