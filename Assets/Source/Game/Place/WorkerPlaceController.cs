using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game.Place
{
    public class WorkerPlaceController : MonoBehaviour
    {
        public Worker CurrentWorker
        {
            get
            {
                return currentWorker;
            }
            set
            {
                currentWorker = value;

                Init();
            }
        }
        private Worker currentWorker;

        public void Init()
        {
            currentWorker.OnRefresh += Refresh;
         
            currentWorker.OnTechnologyball += SpawnTechnologyBall;
            currentWorker.OnDesignball += SpawnDesignBall;
            currentWorker.OnBugBall += SpawnBugBall;
        }

        public void Refresh()
        {

        }

        private void SpawnTechnologyBall()
        {

        }
        private void SpawnDesignBall()
        {

        }
        private void SpawnBugBall()
        {

        }

        private void OnDestroy()
        {
            currentWorker.OnRefresh -= Refresh;

            currentWorker.OnTechnologyball -= SpawnTechnologyBall;
            currentWorker.OnDesignball -= SpawnDesignBall;
            currentWorker.OnBugBall -= SpawnBugBall;
        }
    }
}