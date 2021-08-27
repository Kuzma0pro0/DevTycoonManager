using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DevIdle.Core;
using UnityEngine.UI;

namespace DevIdle.Game.UI
{
    public class StudioScreenController : ScreenController
    {
        public SectionCardController SectionCardPrototype;
        public ScrollRect ScrollRect;

        public override void Init(params object[] param)
        {
            var types = (Enum.GetValues(typeof(SectionType))).OfType<SectionType>();
            foreach (var type in types)
            {
                var panel = Instantiate(SectionCardPrototype);
                panel.transform.SetParent(ScrollRect.content.transform, false);
                panel.Type = type;
            }

            base.Init(param);
        }
    }
}
