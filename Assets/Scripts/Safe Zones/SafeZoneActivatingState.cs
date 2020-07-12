using System;
using UnityEngine;
using UnityEngine.UI;

namespace Safe_Zones {
    public class SafeZoneActivatingState : StateMachineBehaviour
    {
        private Transform activator;
        private float threshold;
        [SerializeField] private float activationTime;
        private float timeLeft;
        private static readonly int Active = Animator.StringToHash("Active");
        private static readonly int Inactive = Animator.StringToHash("Inactive");
        private Slider slider;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            timeLeft = activationTime;
            var safeZone = animator.gameObject.GetComponent<SafeZone>();
            threshold = safeZone.transform.localScale.x / 2f;
            slider = animator.gameObject.transform.root.GetComponentInChildren<Slider>(true);
            slider.gameObject.SetActive(true);
            activator = FindObjectOfType<PlayerMovement>().transform
                ? FindObjectOfType<PlayerMovement>().transform
                : throw new Exception("Unable to find player");
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (ShouldEnterInactive(animator.gameObject.transform.position)) animator.SetTrigger(Inactive);
            if (ShouldEnterActive()) animator.SetTrigger(Active);
            slider.value = (activationTime - timeLeft) / activationTime;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.ResetTrigger(Active);
            slider.gameObject.SetActive(false);
        }

        private bool ShouldEnterActive() {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, activationTime);
            return timeLeft <= 0;
        }
    
        private bool TooFar(Vector3 thisPos) => Vector3.Distance(activator.position, thisPos) > threshold;
        private bool ShouldEnterInactive(Vector3 transformPosition) => TooFar(transformPosition);
    }
}
