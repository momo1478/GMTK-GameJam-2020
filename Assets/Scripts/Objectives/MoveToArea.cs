using UnityEngine;

namespace Objectives {
    public class MoveToArea : Objective {
        private Transform playerTr;
        private Transform targetTr;
        private float range = 10f;
        private float threshold;
        [SerializeField] private GameObject targetPrefab;

        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            playerTr = FindObjectOfType<PlayerMovement>().transform;
            if (targetPrefab == null) targetPrefab = Resources.Load<GameObject>("MoveToTarget");
            targetTr = Instantiate(targetPrefab, new Vector3(Random.Range(-10,10), Random.Range(-10,10), 0), Quaternion.identity).transform;
            targetTr.localScale *= range;
            threshold = range / 2;
        }

        public override bool IsCompleted() {
            if (targetTr == null || playerTr == null) return false;
            return Vector2.Distance(playerTr.position, targetTr.position) <= threshold;
        }

        public override void Cleanup() => targetTr.GetComponent<Target>().Cleanup();

        public override bool IsFailed() => false;
        public override void Completed() {
            // player health += 2
            
            // Manager.AddObjective(gameObject.AddComponent<MoveToArea>());
        }

        public override void Failed() {
            // game.Harder();
        }
    }
}