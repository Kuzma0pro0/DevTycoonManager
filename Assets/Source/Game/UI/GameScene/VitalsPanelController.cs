using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DevIdle.Core;

namespace DevIdle.Game.UI.GameScene
{
    public class VitalsPanelController : MonoBehaviour
    {
        private Player player;

        public TextMeshProUGUI Money;
        public TextMeshProUGUI Design;
        public TextMeshProUGUI Technology;

        private string moneyLabelTemplate;
        private string designLabelTemplate;
        private string technologyLabelTemplate;

        private void Start()
        {
            player = PlayerController.Instance.Player;
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (moneyLabelTemplate == null)
            {
                moneyLabelTemplate = Money.text;
            }
            if (designLabelTemplate == null)
            {
                designLabelTemplate = Design.text;
            }
            if (technologyLabelTemplate == null)
            {
                technologyLabelTemplate = Technology.text;
            }

            Money.text = string.Format(moneyLabelTemplate, player.Studio.Capital);
            Technology.text = string.Format(technologyLabelTemplate, player.Studio.Technology);
            Design.text = string.Format(designLabelTemplate, player.Studio.Design);
        }

    }
}