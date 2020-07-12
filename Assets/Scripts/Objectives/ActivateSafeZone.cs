using System;
using Safe_Zones;
using UnityEngine;

namespace Objectives {
    public class ActivateSafeZone : Objective {
        private SafeZone safeZone;
        private float timeLeft;
        [SerializeField] private float timeToComplete = 10f;
        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            safeZone = Instantiate(Resources.Load<SafeZone>("SafeZone/SafeZone"));
            safeZone.transform.position = Utils.Utils.RandomPositionOnBoard();
            timeLeft = timeToComplete;
        }

        private void Update() {
            if (safeZone.HasActivated() || safeZone == null) return;
            
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, timeToComplete);
        }

        public override bool IsCompleted() => safeZone.IsClosed();

        public override void Cleanup() {
            Destroy(safeZone.gameObject);
        }

        public override bool IsFailed() => timeLeft <= 0;

        public override void Completed() {
            Manager.AddObjective(gameObject.AddComponent<ActivateSafeZone>());
            GameManager.AddScore(scoreReward);
            DisplayText($"+{scoreReward}", safeZone.transform.position);
        }

        public override void Failed() {
        }
    }
}