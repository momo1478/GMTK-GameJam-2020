using UnityEngine;

namespace Objectives {
    public abstract class SurviveUntil : Objective {
                
        protected float startTime = 0;
    
        protected override void Awake() {
            timeToComplete = 4f;
            base.Awake();
        }
        
        protected virtual void Start() {
            startTime = Time.time;
            DisplayName = "Survive";
        }

        public override bool IsCompleted() {
            lapsedTime = (Time.time - startTime);
            return  lapsedTime > timeToComplete;
        }

        public override void Cleanup() {

        }

        public override bool IsFailed() => false;

        public override void Failed() {
            
        }
    }
}