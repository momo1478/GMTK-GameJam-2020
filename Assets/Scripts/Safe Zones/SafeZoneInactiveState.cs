using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZoneInactiveState : SafeZoneState {
        private Transform activator;
        private float threshold;
        private static readonly int Activating = Animator.StringToHash("Activating");

        private bool ShouldExit(Vector3 thisPos) {
            if (activator == null) return false;
            return !TooFar(thisPos);
        }
        private bool TooFar(Vector3 thisPos) => Vector3.Distance(activator.position, thisPos) > threshold;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            var safeZone = animator.gameObject.GetComponent<SafeZone>();
            threshold = safeZone.transform.localScale.x  / 2;
            activator = FindObjectOfType<PlayerMovement>().transform
                ? FindObjectOfType<PlayerMovement>().transform
                : throw new Exception("Unable to find player");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldExit(animator.gameObject.transform.position)) animator.SetTrigger(Activating);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.ResetTrigger(Activating);
        }
    }
}