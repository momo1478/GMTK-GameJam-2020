﻿using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZone : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private CircleCollider2D collider2D;
        
        private bool IsInactive () => animator.GetCurrentAnimatorStateInfo(0).IsName("Inactive");
        private bool IsActivating () => animator.GetCurrentAnimatorStateInfo(0).IsName("Activating");
        private bool IsActive () => animator.GetCurrentAnimatorStateInfo(0).IsName("Active");
        private bool IsClosing () => animator.GetCurrentAnimatorStateInfo(0).IsName("IsClosing");
        private bool IsClosed () => animator.GetCurrentAnimatorStateInfo(0).IsName("IsClosed");
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