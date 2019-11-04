using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game.Place
{
    [Serializable]
    public class StudioPrefabInfo
    {
        [SerializeField]
        public StudioTheme Theme;
        [SerializeField]
        public StudioPlaceController Prefab;
    }

    public class PlaceController : MonoBehaviour
    {
        public static PlaceController Instance { get; private set; }

        private Player player;

        public List<StudioPrefabInfo> StudioPrefabs = new List<StudioPrefabInfo>();

        public GameObject StudioContainer;

        private StudioTheme currentStudioPrefab;

        private void Start()
        {
            Instance = this;

            player = PlayerController.Instance.Player;

            ReloadStudioPrefab();
        }

        public void ReloadStudioPrefab()
        {
            if (this.StudioPrefabs.Count == 0)
            {
                return;
            }

            var info = StudioPrefabs.Single((x) => x.Theme == player.Studio.Theme);

            for (int i = 0; i < StudioContainer.transform.childCount; ++i)
            {
                Destroy(StudioContainer.transform.GetChild(i).gameObject);
            }

            var prefab = Instantiate(info.Prefab, StudioContainer.transform);
            currentStudioPrefab = info.Theme;
        }
    }
}