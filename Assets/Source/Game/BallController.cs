using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DevIdle.Core;

namespace DevIdle.Game
{
    public class BallController : MonoBehaviour
    {
        public Color BugColor;
        public Color TechnologyColor;
        public Color DesignColor;

        private Animation anim;

        private void Start()
        {
            anim = GetComponent<Animation>();
        }

        public void SetColor(BallType type)
        {
            switch (type) {
                case BallType.Technology: {
                        GetComponent<Image>().color = TechnologyColor;
                        break;
                    }
                case BallType.Design:
                    {
                        GetComponent<Image>().color = DesignColor;
                        break;
                    }
                case BallType.Bug:
                    {
                        GetComponent<Image>().color = BugColor;
                        break;
                    }
            }
        }

        public void SetPosition(BallType type)
        {
            switch (type)
            {
                case BallType.Technology:
                    {
                        GetComponent<RectTransform>().localPosition = new Vector2(-30, 75);
                        break;
                    }
                case BallType.Design:
                    {
                        GetComponent<RectTransform>().localPosition = new Vector2(30, 75);
                        break;
                    }
                case BallType.Bug:
                    {
                        GetComponent<RectTransform>().localPosition = new Vector2(0, 50);
                        break;
                    }
            }
        }

        public void PlayAnimation()
        {
            if (anim == null) {
                anim = GetComponent<Animation>();
            }

            anim.Play();
        }

        public float GetAnimationLength(string text)
        {
            return GetComponent<Animation>().GetClip(text).length;
        }

        public void SetText(string text)
        {
           var component = GetComponentInChildren<Text>();
            if (component != null) {
                component.text = text;
            }
        }
    }
}