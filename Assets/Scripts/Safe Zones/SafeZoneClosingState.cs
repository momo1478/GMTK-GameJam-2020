using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZoneClosingState : StateMachineBehaviour {
        private float timeLeft;
        [SerializeField] private float activeDuration;
        private static readonly int Closed = Animator.StringToHash("Closed");
        private CircleCollider2D collider;
        private ParticleSystem particleSystem;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            timeLeft = activeDuration;
            collider = animator.gameObject.GetComponent<CircleCollider2D>() ??
                       throw new Exception($"Unable to assign colider2d in {name}");
            particleSystem = animator.gameObject.GetComponentInChildren<ParticleSystem>() ??
                 throw new Exception($"Unable to assign particle system in {name}");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldEnterClosed()) animator.SetTrigger(Closed);
            collider.radius = Mathf.Lerp(collider.radius, 0, timeLeft);
            SetParticleSystemRadius();
        }

        private void SetParticleSystemRadius() {
            var shape = particleSystem.shape;
            shape.radius = collider.radius;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.ResetTrigger(Closed);
            particleSystem.Stop();
            particleSystem.Clear();
            Destroy(particleSystem.gameObject);
        }

        private bool ShouldEnterClosed() {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, activeDuration);
            return timeLeft <= 0;
            ;
        }
    }
}