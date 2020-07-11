using System;
using UnityEngine;

namespace Safe_Zones {
    public class SafeZone : MonoBehaviour {
        [SerializeField] private readonly float range;
        public float Range => range;
        [SerializeField] private GameObject vfx;
        [SerializeField] private Animator animator;
        public static Action<Collider2D, GameObject> HandleTriggerEnter2D = delegate {  };

        private void OnTriggerEnter2D(Collider2D other) {
            HandleTriggerEnter2D(other, gameObject);
        }
    }
}