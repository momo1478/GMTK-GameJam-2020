using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZoneClosingState : StateMachineBehaviour {
        private float timeLeft;
        [SerializeField] private float activeDuration;
        private static readonly int Closed = Animator.StringToHash("Closed");
        private CircleCollider2D collider;
        private ParticleSystem particleSystem;
        private float lerpTimeLeft;
        private float lerpDuration;
        private float maxColliderSize;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            timeLeft = activeDuration;
            lerpDuration = activeDuration;
            lerpTimeLeft = lerpDuration;
            collider = animator.gameObject.GetComponent<CircleCollider2D>() ??
                       throw new Exception($"Unable to assign colider2d in {name}");
            particleSystem = animator.gameObject.GetComponentInChildren<ParticleSystem>() ??
                 throw new Exception($"Unable to assign particle system in {name}");
            maxColliderSize = collider.radius;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldEnterClosed()) animator.SetTrigger(Closed);

            HandleColliderLerp();
            SetParticleSystemRadius();
        }
        
        
        private void HandleColliderLerp() {
            lerpTimeLeft = Mathf.Clamp(lerpTimeLeft - Time.deltaTime, 0, lerpDuration);
            var lerpFrac = (lerpDuration - lerpTimeLeft) / lerpDuration;
            collider.radius = Mathf.Lerp(maxColliderSize, 0, lerpFrac);
        }

        private void SetParticleSystemRadius() {
            var shape = particleSystem.shape;
            shape.radius = collider.gameObject.transform.localScale.x * collider.radius;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.ResetTrigger(Closed);
            particleSystem.Stop();
            particleSystem.Clear();
            Destroy(particleSystem.gameObject);
            Destroy(animator.gameObject);
        }

        private bool ShouldEnterClosed() {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, activeDuration);
            return timeLeft <= 0;
        }
    }
}