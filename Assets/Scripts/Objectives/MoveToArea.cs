using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class MoveToArea : Objective {
        private Transform playerTr;
        private Transform targetTr;
        private float range = 3f;
        private float threshold;
        [SerializeField] private GameObject targetPrefab;
        
        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            playerTr = FindObjectOfType<PlayerMovement>().transform;
            if (targetPrefab == null) targetPrefab = Resources.Load<GameObject>("MoveToTarget");
            targetTr = Instantiate(targetPrefab, Utils.Utils.RandomPositionOnBoard(),
                Quaternion.identity).transform;
            targetTr.localScale *= Random.Range(range/2f, range);
            threshold = range / 2;
            targetTr.SetParent(gameObject.transform);
            DisplayName = "Move To Area";
        }

        private void Update() {
            lapsedTime += Time.deltaTime;
        }

        public override bool IsCompleted() {
            if (targetTr == null || playerTr == null) return false;
            return Vector2.Distance(playerTr.position, targetTr.position) <= threshold;
        }

        public override void Cleanup() => targetTr.GetComponent<Target>().Cleanup();

        public override bool IsFailed() => lapsedTime > timeToComplete;

        public override void Completed() {
            // TODO: Increment score
            Manager.AddObjective(gameObject.AddComponent<MoveToArea>());
            GameManager.AddScore(scoreReward);
            DisplayText($"+{scoreReward}", targetTr.position);
        }

        public override void Failed() {
            GameManager.instance.Damage(5);
            RenderFailMessage();
        }
        
        private void RenderFailMessage() {
            var player = FindObjectOfType<PlayerMovement>();
            if (player == null) return;
            DisplayText($"Objective Failed!", player.transform.position);
        }
    }
}