using System.Collections.Generic;
using UnityEngine;

namespace Tasks {
    public class ObjectiveManager : MonoBehaviour {
        public List<Objective> Objectives {get; private set;}
        public List<Objective> ClearedObjectives {get; private set;}

        private void Awake() {
            Objectives = new List<Objective>();
            ClearedObjectives = new List<Objective>();
        }

        private void Update() {
            foreach (var t in Objectives) HandleCompleted(t);
        }

        private void HandleCompleted(Objective t) {
            if (t.IsCompleted()) {
                ClearedObjectives.Add(t);
            }
        }

        private void LateUpdate() {
            if (ClearedObjectives.Count <= 0) return;
            
            for (int i = ClearedObjectives.Count - 1; i >= 0; i--) {
                var t = ClearedObjectives[i];
                Objectives.Remove(t);
                ClearedObjectives.Remove(t);
                Destroy(t);
            }
        }

        public void AddTask(Objective t) => Objectives.Add(t);

        public void RemoveTask(Objective t) => ClearedObjectives.Add(t);
    }
}