using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Safe_Zones {
    public class SafeZoneActiveState : StateMachineBehaviour {
        private float stateTimeLeft;
        [SerializeField] private float activeDuration;
        private static readonly int Closing = Animator.StringToHash("Closing");
        private float range = 1.2f;
        private CircleCollider2D collider;
        private float lerpTimeLeft;
        private float lerpDuration = .75f;
        private ParticleSystem particleSystem;
        [SerializeField] private GameObject particleSystemObject;
        private float minColliderRadius;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller) {
            stateTimeLeft = activeDuration;
            lerpDuration = activeDuration;
            lerpTimeLeft = lerpDuration;
            collider = animator.gameObject.GetComponent<CircleCollider2D>() ??
                       throw new Exception($"Unable to assign colider2d in {name}");
            var go = Instantiate(particleSystemObject, animator.gameObject.transform);
            go.transform.localPosition = Vector3.zero;

            minColliderRadius = 1f;
            
            particleSystem = go.GetComponent<ParticleSystem>() ??
                             throw new Exception($"Unable to assign particle system in {name}");
            
            animator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldEnterClosing()) animator.SetTrigger(Closing);

            HandleColliderLerp();
            HandleParticleSystemLerp();
        }

        private void HandleParticleSystemLerp() {
            var shape = particleSystem.shape;
            shape.radius = collider.gameObject.transform.localScale.x * collider.radius;
        }

        private void HandleColliderLerp() {
            lerpTimeLeft = Mathf.Clamp(lerpTimeLeft - Time.deltaTime, 0, lerpDuration);
            var lerpFrac = (lerpDuration - lerpTimeLeft) / lerpDuration;
            collider.radius = Mathf.Lerp(minColliderRadius, range, lerpFrac);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.ResetTrigger(Closing);
        }

        private bool ShouldEnterClosing() {
            stateTimeLeft = Mathf.Clamp(stateTimeLeft - Time.deltaTime, 0, activeDuration);
            return stateTimeLeft <= 0;
            ;
        }
    }
}