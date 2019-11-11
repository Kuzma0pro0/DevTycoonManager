using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;
using UnityEngine.UI;

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
                var temp = value;

                if (currentWorker == null)
                {
                    currentWorker = value;
                    Init();
                }
                else
                {
                    OnDestroy();

                    currentWorker = value;

                    Init();
                }

            }
        }
        private Worker currentWorker;

        public Image Avatar;

        public GameObject PrefabBalls;
        public GameObject ParentBalls;

        private List<BallController> PoolTechnologyBall = new List<BallController>();
        private List<BallController> PoolDesignBall = new List<BallController>();
        private List<BallController> PoolBugBall = new List<BallController>();

        int TechnologyCounter;
        int DesignCounter;
        int BugCounter;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                var ball = Instantiate(PrefabBalls, ParentBalls.transform).GetComponent<BallController>();
                ball.SetColor(BallType.Technology);
                ball.SetPosition(BallType.Technology);
                PoolTechnologyBall.Add(ball);
            }

            for (int i = 0; i < 10; i++)
            {
                var ball = Instantiate(PrefabBalls, ParentBalls.transform).GetComponent<BallController>();
                ball.SetColor(BallType.Design);
                ball.SetPosition(BallType.Design);
                PoolDesignBall.Add(ball);
            }

            for (int i = 0; i < 10; i++)
            {
                var ball = Instantiate(PrefabBalls, ParentBalls.transform).GetComponent<BallController>();
                ball.SetColor(BallType.Bug);
                ball.SetPosition(BallType.Bug);
                PoolBugBall.Add(ball);
            }
        }

        private void Update()
        {
           
        }

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
            if (PoolTechnologyBall.Count <= 0)
            {
                return;
            }

            PoolTechnologyBall[TechnologyCounter].PlayAnimation();
            PoolTechnologyBall[TechnologyCounter].SetText("+1");

            TechnologyCounter = TechnologyCounter == PoolTechnologyBall.Count - 1 ? 0 : TechnologyCounter + 1;
        }
        private void SpawnDesignBall()
        {
            if (PoolDesignBall.Count <= 0)
            {
                return;
            }

            PoolDesignBall[DesignCounter].PlayAnimation();
            PoolDesignBall[DesignCounter].SetText("+1");

            DesignCounter = DesignCounter == PoolDesignBall.Count - 1 ? 0 : DesignCounter + 1;
        }
        private void SpawnBugBall()
        {
            if (PoolBugBall.Count <= 0)
            {
                return;
            }

            PoolBugBall[BugCounter].PlayAnimation();
            PoolBugBall[BugCounter].SetText("+1");

            BugCounter = BugCounter == PoolBugBall.Count - 1 ? 0 : BugCounter + 1;
        }

        private void OnDestroy()
        {
            if (currentWorker != null)
            {
                currentWorker.OnRefresh -= Refresh;

                currentWorker.OnTechnologyball -= SpawnTechnologyBall;
                currentWorker.OnDesignball -= SpawnDesignBall;
                currentWorker.OnBugBall -= SpawnBugBall;
            }
        }
    }
}