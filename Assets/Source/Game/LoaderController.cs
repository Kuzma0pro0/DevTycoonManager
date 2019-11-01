using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevIdle.Game
{
    public class LoaderController : MonoBehaviour
    {
        private void Start()
        {
            StudioController.Instance.Studio = App.Profile.Studio;

            Destroy(gameObject);
        }
    }
}