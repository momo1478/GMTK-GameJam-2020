using System;
using Objectives;
using TMPro;
using UnityEngine;

namespace UI {
    public class ObjectiveListItem : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI nameUgui;
        [SerializeField] private TextMeshProUGUI timeUgui;
        private float timeleft;
        private string name = "";
        private Objective objective;

        private void Update() {
            if (objective != null) {
                if (name == "") {
                    if (objective.DisplayName == "") return;
                    
                    name = objective.DisplayName;
                    nameUgui.SetText(name);
                }

                timeleft = (int)(objective.timeToComplete - objective.lapsedTime);
                timeUgui.SetText(timeleft.ToString());
            }
        }

        public void SetObjective(Objective objective) {
            this.objective = objective;
        }
    }
}