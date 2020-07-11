using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Safe_Zones {
    public class SafeZoneActiveState : StateMachineBehaviour {
        private float stateTimeLeft;
        [SerializeField] private float activeDuration;
        private static readonly int Closing = Animator.StringToHash("Closing");
        [SerializeField] private float range;
        private CircleCollider2D collider;
        private float lerpTimeLeft;
        [SerializeField] private float lerpDuration = .75f;
        private ParticleSystem particleSystem;
        [SerializeField] private GameObject particleSystemObject;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller) {
            stateTimeLeft = activeDuration;
            lerpTimeLeft = lerpDuration;
            collider = animator.gameObject.GetComponent<CircleCollider2D>() ??
                       throw new Exception($"Unable to assign colider2d in {name}");
            var go = Instantiate(particleSystemObject, animator.gameObject.transform);
            go.transform.localPosition = Vector3.zero;
            
            particleSystem = go.GetComponent<ParticleSystem>() ??
                             throw new Exception($"Unable to assign particle system in {name}");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldEnterClosing()) animator.SetTrigger(Closing);

            HandleColliderLerp();
            HandleParticleSystemLerp();
        }

        private void HandleParticleSystemLerp() {
            var shape = particleSystem.shape;
            shape.radius = collider.radius;
        }

        private void HandleColliderLerp() {
            lerpTimeLeft = Mathf.Clamp(lerpTimeLeft - Time.deltaTime, 0, lerpDuration);
            collider.radius = Mathf.Lerp(collider.radius, range, lerpTimeLeft);
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