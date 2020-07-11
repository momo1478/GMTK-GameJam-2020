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
            var ob = gameObject.AddComponent<MoveToArea>();
            var ob1 = gameObject.AddComponent<MoveToArea>();
            AddObjective(ob);
            AddObjective(ob1);
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
                var t = FailedObjectives[i];
                t.Failed();
                t.Cleanup();
                Objectives.Remove(t);
                FailedObjectives.Remove(t);
                Destroy(t);
            }
        }
        
        public void AddObjective(Objective t) => Objectives.Add(t);

        public void RemoveObjective(Objective t) {
            t.Cleanup();
            Objectives.Remove(t);
            Destroy(t);
        }
    }
}