using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZone : MonoBehaviour {
        [SerializeField] private readonly float range;
        public float Range => range;
        [SerializeField] private GameObject vfx;
        [SerializeField] private Animator animator;
        
        private void Update() {
            // print($"Inactive: {animator.GetCurrentAnimatorStateInfo(0).IsName("Inactive")}");
            // print($"Activating: {animator.GetCurrentAnimatorStateInfo(0).IsName("Activating")}");
            // print($"Active: {animator.GetCurrentAnimatorStateInfo(0).IsName("Active")}");
        }

        private void OnTriggerEnter2D(Collider2D other) {
            print("Enter2D");
        }
    }
}