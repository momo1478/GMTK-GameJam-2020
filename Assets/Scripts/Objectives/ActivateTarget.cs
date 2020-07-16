using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateTarget : Objective {
        private float lapsedTime = 0f;
        [SerializeField] private float timeToComplete = 10f;
        [SerializeField] private float maxCharge = 1.5f;
        private ChargeStation chargeStation;

        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            chargeStation = Instantiate(Resources.Load<ChargeStation>("ChargeStation"),
                Utils.Utils.RandomPositionOnBoard(), Quaternion.identity);
            var scale = Random.Range(5, 15);
            chargeStation.transform.localScale *= scale;
            chargeStation.AssignObjective(maxCharge);
            scoreReward = scoreReward * 3 / 2;
        }

        private void Update() {
            lapsedTime += Time.deltaTime;
        }

        public override bool IsCompleted() => chargeStation?.Charged ?? false;

        public override void Cleanup() {
            chargeStation.GetComponent<Target>().Cleanup();
            Destroy(chargeStation.gameObject);
        }

        public override bool IsFailed() {
            return lapsedTime > timeToComplete;
        }

        public override void Completed()
        {
            Manager.AddObjective(gameObject.AddComponent<ActivateTarget>());
            GameManager.AddScore(scoreReward);
            DisplayText($"+{scoreReward}", chargeStation.transform.position);
        }

        public override void Failed() {
            print("target");
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