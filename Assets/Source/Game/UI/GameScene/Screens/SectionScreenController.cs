using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DevIdle.Core;

namespace DevIdle.Game.UI
{
    public class SectionScreenController : ScreenController
    {
        public TextMeshProUGUI Level;
        public TextMeshProUGUI ExperienceLevel;
        public TextMeshProUGUI CountWorker;

        private Section section;

        private string levelLabelTemplate;
        private string experienceLevelLabelTemplate;
        private string countWorkerLabelTemplate;

        private Studio studio;

        private Studio GetStudio()
        {
            if (studio == null)
            {
                studio = FindObjectOfType<PlayerController>().Player.Studio;
            }

            return studio;
        }

        public override void Init(params object[] param)
        {
            if (param.Length == 0 || (section = param[0] as Section) == null)
            {
                throw new ArgumentException($"Invalid parameters list. First element must be {typeof(Section)}");
            }

            Refresh();
            base.Init(param);
        }

        public void UpgradeSection()
        {
            if (GetStudio().TryUpdateSection(section))
            {
                Refresh();
            }
        }

        public void HireNextWorker()
        {

        }

        public void UpgradeExperienceLevel()
        {

        }

        private void Refresh()
        {
            if (levelLabelTemplate == null)
            {
                levelLabelTemplate = Level.text;
            }
            if (experienceLevelLabelTemplate == null)
            {
                experienceLevelLabelTemplate = ExperienceLevel.text;
            }
            if (countWorkerLabelTemplate == null)
            {
                countWorkerLabelTemplate = CountWorker.text;
            }

            Level.text = string.Format(levelLabelTemplate, section.Level);
            ExperienceLevel.text = string.Format(experienceLevelLabelTemplate, section.ExperienceLevel);
            CountWorker.text = string.Format(countWorkerLabelTemplate, section.Workers, section.MaxWorker);
        }
    }
}