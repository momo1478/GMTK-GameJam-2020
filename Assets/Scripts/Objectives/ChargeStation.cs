using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Objectives {
    public class ChargeStation : MonoBehaviour {
        [SerializeField] private float maxCharge;
        private float chargeLeft;
        private bool inRange;
        public bool Charged { get; private set; }
        [SerializeField] private Slider slider;

        private void Update() {
            if (!Input.GetKey(KeyCode.F) || !inRange) return;

            chargeLeft = Mathf.Clamp(chargeLeft - Time.deltaTime, 0, maxCharge);
            slider.value = (maxCharge - chargeLeft) / maxCharge;
            
            if (chargeLeft <= 0) Charged = true;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            inRange = true;
            slider.gameObject.SetActive(true);
            slider.enabled = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            inRange = false;
            slider.gameObject.SetActive(false);
        }

        public void AssignObjective(float charge) {
            maxCharge = charge;
            chargeLeft = maxCharge;
        }
    }
}