using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game
{
    public class StudioController : MonoBehaviour
    {
        public static StudioController Instance { get; private set; }

        public Studio Studio
        {
            get
            {
                return studio;
            }
            set
            {
                studio = value;

                studio.Init();
            }
        }

        public Studio studio;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            Studio.Update(Time.deltaTime);
        }
    }
}