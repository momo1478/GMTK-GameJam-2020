using UnityEngine;

namespace Objectives {
    public class ActivateTarget : Objective {
        private float chargeLeft;
        private float timeLeft;
        [SerializeField] private float maxCharge;
        [SerializeField] private float timeToComplete;
        public override bool IsCompleted() => chargeLeft <= 0;

        public override void Cleanup() {
            throw new System.NotImplementedException();
        }

        public override bool IsFailed() {
            throw new System.NotImplementedException();
        }

        public override void Completed() {
            throw new System.NotImplementedException();
        }

        public override void Failed() {
            throw new System.NotImplementedException();
        }
    }
}