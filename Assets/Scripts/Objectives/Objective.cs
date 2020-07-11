using UnityEngine;

namespace Objectives {
    [RequireComponent(typeof(ObjectiveManager))]
    public abstract class Objective : MonoBehaviour {
        protected ObjectiveManager Manager;
        public abstract bool IsCompleted();
        public abstract void Cleanup();
        public abstract bool IsFailed();

        public abstract void Completed();

        public abstract void Failed();
    }
}