using System;
using UnityEngine;
using UnityEngine.UI;

namespace Safe_Zones {
    public class SafeZone : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private CircleCollider2D collider2D;
        [SerializeField] private Slider slider;
        private bool IsInactive () => animator.GetCurrentAnimatorStateInfo(0).IsName("Inactive");
        private bool IsActivating () => animator.GetCurrentAnimatorStateInfo(0).IsName("Activating");
        public bool IsActive () => animator.GetCurrentAnimatorStateInfo(0).IsName("Active");
        private bool IsClosing () => animator.GetCurrentAnimatorStateInfo(0).IsName("Closing");
        public bool IsClosed () => animator.GetCurrentAnimatorStateInfo(0).IsName("Closed");

        public bool HasActivated() => IsActive() || IsClosed() || IsClosing();
        private void OnTriggerEnter2D(Collider2D other) {
            if (IsActivating() || IsInactive() || IsClosed()) return;
            
            if (other.gameObject.GetComponent<Projectile>()) Destroy(other.gameObject);
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (IsActivating() || IsInactive() || IsClosed()) return;

            if (other.gameObject.GetComponent<Projectile>()) Destroy(other.gameObject);
        }
    }
}