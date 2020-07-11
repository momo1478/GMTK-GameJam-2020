using System;
using UnityEngine;

namespace Tasks {
    public class MoveToArea : Objective {
        private Transform selfTr;
        private Transform targetTr;
        private float threshold = 1f;
        private void Awake() {
            selfTr = gameObject.transform;
            targetTr = FindObjectOfType<Target>().transform ? FindObjectOfType<Target>().transform : throw new Exception("No Target Found");
        }

        public override bool IsCompleted() => Vector2.Distance(selfTr.position, targetTr.position) <= threshold;
    }
}