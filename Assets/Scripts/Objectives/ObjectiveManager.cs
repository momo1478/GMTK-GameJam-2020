using System.Collections.Generic;
using UnityEngine;

namespace Tasks {
    public class ObjectiveManager : MonoBehaviour {
        private List<Objective> tasks;
        private List<Objective> clearedTasks;

        private void Awake() {
            tasks = new List<Objective>();
            clearedTasks = new List<Objective>();
        }

        private void Update() {
            foreach (var t in tasks) HandleCompleted(t);
        }

        private void HandleCompleted(Objective t) {
            if (t.IsCompleted()) {
                clearedTasks.Add(t);
            }
        }

        private void LateUpdate() {
            if (clearedTasks.Count <= 0) return;
            
            for (int i = clearedTasks.Count - 1; i >= 0; i--) {
                var t = clearedTasks[i];
                tasks.Remove(t);
                clearedTasks.Remove(t);
                Destroy(t);
            }
        }

        public void AddTask() { }

        public void RemoveTask(Objective t) => clearedTasks.Add(t);
    }
}