using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class Juicer : MonoBehaviour {
        private float juice1TimeLeft = 0f;

        private float juice2TimeLeft = 0f;
        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        private void Start() {
            HandleJuice();
        }

        void Update() {
            // if (juice1TimeLeft <= 0) HandleJuice();
        }

        private void HandleJuice() {
            // juice1TimeLeft = Random.Range(1,3f);
            animator.Play("Juice", 1);
        }
    }
}
