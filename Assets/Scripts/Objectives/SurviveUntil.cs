using UnityEngine;

namespace Objectives {
    public abstract class SurviveUntil : Objective {
                
        protected float startTime = 0;
        [SerializeField] public float timeToComplete = 10f;  // sec

        protected virtual void Start() {
            startTime = Time.time;
        }

        public override bool IsCompleted()
        {
            return (Time.time - startTime) > timeToComplete;
        }

        public override void Cleanup() {

        }

        public override bool IsFailed() => false;

        public override void Failed() {
            
        }
    }
}