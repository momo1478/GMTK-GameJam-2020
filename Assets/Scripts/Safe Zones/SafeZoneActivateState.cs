using Safe_Zones;
using UnityEngine;
using UnityEngine.Animations;

public class SafeZoneActivateState : StateMachineBehaviour {
    private Collider collider;
    private Animator Animator { get; set; }
    private float timeLeft;
    [SerializeField] private float activeDuration;
    private static readonly int Closing = Animator.StringToHash("Closing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller) {
        timeLeft = activeDuration;
        Animator = animator;
        SafeZone.HandleTriggerEnter2D += HandleTriggerEnter2D;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       if (ShouldEnterClosing()) Animator.SetTrigger(Closing);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SafeZone.HandleTriggerEnter2D -= HandleTriggerEnter2D;
        Animator.ResetTrigger(Closing);
    }

    private void HandleTriggerEnter2D(Collider2D other, GameObject arg2) {
        if (Animator.gameObject != arg2) return;
        
        if (other.gameObject.GetComponent<Projectile>()) Destroy(other.gameObject);
    }
    
    private bool ShouldEnterClosing() {
        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, activeDuration);
        Debug.Log(timeLeft);
        return timeLeft <= 0;;
    }
}