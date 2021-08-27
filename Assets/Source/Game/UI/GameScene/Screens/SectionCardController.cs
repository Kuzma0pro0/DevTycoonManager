using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DevIdle.Core;
using UnityEngine.UI;
using TMPro;

namespace DevIdle.Game.UI
{
    public class SectionCardController : MonoBehaviour
    {
        private Studio studio;
        private SectionType type;

        public SectionType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;

                Init();
            }
        }

        private Studio GetStudio()
        {
            if (studio == null)
            {
                studio = FindObjectOfType<PlayerController>().Player.Studio;
            }

            return studio;
        }

        public TextMeshProUGUI Name;

        public TextMeshProUGUI Button;

        public Button BuyButton;

        private void Init() 
        {
            BuyButton.onClick.AddListener(BuySection);

            Refresh();
        }

        public void BuySection() 
        {
            var result = GetStudio().TryBuySection(Type);
            if (result)
            {
                Refresh();
            }
        }

        private void Refresh()
        {

        }

        private void OnDestroy()
        {
            BuyButton.onClick.RemoveListener(BuySection);
        }

    }
}