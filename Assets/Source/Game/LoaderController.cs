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
            PlayerController.Instance.Player = App.Profile.Player;

            Destroy(gameObject);
        }
    }
}