using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class MoveToArea : Objective {
        private Transform playerTr;
        private Transform targetTr;
        private float range = 10f;
        private float threshold;
        [SerializeField] private GameObject targetPrefab;
        private float timeLeft;
        [SerializeField] private float timeToComplete = 10f;

        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            playerTr = FindObjectOfType<PlayerMovement>().transform;
            if (targetPrefab == null) targetPrefab = Resources.Load<GameObject>("MoveToTarget");
            targetTr = Instantiate(targetPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0),
                Quaternion.identity).transform;
            targetTr.localScale *= range;
            threshold = range / 2;
            timeLeft = timeToComplete;
        }

        private void Update() {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, timeToComplete);
        }

        public override bool IsCompleted() {
            if (targetTr == null || playerTr == null) return false;
            return Vector2.Distance(playerTr.position, targetTr.position) <= threshold;
        }

        public override void Cleanup() => targetTr.GetComponent<Target>().Cleanup();

        public override bool IsFailed() => timeLeft <= 0;

        public override void Completed() {
            DisplayText($"+{scoreReward}", targetTr.position);
            // Manager.AddObjective(gameObject.AddComponent<MoveToArea>());
        }

        public override void Failed() {
            // game.Harder();
        }
    }
}