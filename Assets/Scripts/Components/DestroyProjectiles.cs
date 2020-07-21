using System;
using UnityEngine;

namespace Components {
    [RequireComponent(typeof(Collider2D))]
    public class DestroyProjectiles : MonoBehaviour {
        private void OnTriggerExit(Collider other) {
            var projComponent = other.gameObject.GetComponent<Projectile>();
            if (projComponent != null) Destroy(projComponent.gameObject);
        }
    }
}