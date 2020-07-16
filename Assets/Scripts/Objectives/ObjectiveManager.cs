using System.Collections.Generic;
using UnityEngine;

namespace Objectives {
    public class ObjectiveManager : MonoBehaviour {
        public List<Objective> Objectives {get; private set;}
        public List<Objective> ClearedObjectives {get; private set;}
        public List<Objective> FailedObjectives {get; private set;}

        private void Awake() {
            Objectives = new List<Objective>();
            ClearedObjectives = new List<Objective>();
            FailedObjectives = new List<Objective>();
        }

        private void Update() {
            foreach (var o in Objectives) UpdateObjectives(o);
        }

        private void UpdateObjectives(Objective o) {
            if (o.IsCompleted()) ClearedObjectives.Add(o);
            if (o.IsFailed()) FailedObjectives.Add(o);
        }


        private void LateUpdate() {
            if (ClearedObjectives.Count > 0) HandleCompleted();
            if (FailedObjectives.Count > 0) HandleFailed();
        }

        private void HandleCompleted() {
            for (int i = ClearedObjectives.Count - 1; i >= 0; i--) {
                var o = ClearedObjectives[i];
                o.Completed();
                o.Cleanup();
                Objectives.Remove(o);
                ClearedObjectives.Remove(o);
                Destroy(o);
            }
        }

        private void HandleFailed() {
            for (int i = FailedObjectives.Count - 1; i >= 0; i--) {
                var o = FailedObjectives[i];
                o.Failed();
                o.Cleanup();
                Objectives.Remove(o);
                FailedObjectives.Remove(o);
                if (o) Destroy(o);
            }
        }
        
        public void AddObjective(Objective o) => Objectives.Add(o);

        public void RemoveObjective(Objective o) {
            Objectives.Remove(o);
            o.Cleanup();
            if (o) Destroy(o);
        }
    }
}