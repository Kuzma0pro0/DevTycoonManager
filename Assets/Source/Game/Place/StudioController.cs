using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game.Place
{
    public class StudioController : MonoBehaviour
    {
        private Studio studio;

        private void Start()
        {
            studio = PlayerController.Instance.Player.Studio;
        }

        public void Refresh()
        {

        }
    }
}