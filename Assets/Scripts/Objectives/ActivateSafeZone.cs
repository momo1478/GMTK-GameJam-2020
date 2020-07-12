using System;
using Safe_Zones;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateSafeZone : Objective {
        private SafeZone safeZone;
        private float timeLeft;
        [SerializeField] private float timeToComplete = 10f;
        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            safeZone = Instantiate(Resources.Load<SafeZone>("SafeZone/SafeZone"));
            safeZone.transform.position = Utils.Utils.RandomPositionOnBoard();
            var scale = Random.Range(5, 15);
            safeZone.transform.localScale = new Vector3(scale,scale,1);
            timeLeft = timeToComplete;
        }

        private void Update() {
            if (safeZone.HasActivated() || safeZone == null) return;
            
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, timeToComplete);
        }

        public override bool IsCompleted()
        {
            if (safeZone == null) return false;
            return safeZone.IsClosed();
        } 

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