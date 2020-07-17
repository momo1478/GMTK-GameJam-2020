using System;
using Safe_Zones;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateSafeZone : Objective {
        private SafeZone safeZone;
        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            safeZone = Instantiate(Resources.Load<SafeZone>("SafeZone/SafeZone"), gameObject.transform, true);
            safeZone.transform.position = Utils.Utils.RandomPositionOnBoard();
            var scale = Random.Range(5, 15);
            safeZone.transform.localScale = new Vector3(scale,scale,1);
            DisplayName = "Safe Zone";

        }

        private void Update() {
            if (safeZone.HasActivated() || safeZone == null) return;
            
            lapsedTime += Time.deltaTime;
        }

        public override bool IsCompleted()
        {
            if (safeZone == null) return false;
            return safeZone.IsClosed();
        } 

        public override void Cleanup() {
            Destroy(safeZone.gameObject);
        }

        public override bool IsFailed() {
            return lapsedTime > timeToComplete;
        }

        public override void Completed() {
            Manager.AddObjective(gameObject.AddComponent<ActivateSafeZone>());
            GameManager.AddScore(scoreReward);
            DisplayText($"+{scoreReward}", safeZone.transform.position);
        }

        public override void Failed() {
            GameManager.instance.Damage(1);
            RenderFailMessage();
        }
        
        private void RenderFailMessage() {
            var player = FindObjectOfType<PlayerMovement>();
            if (player == null) return;
            var pos = player.transform.position;
            pos.y += 2f;
            DisplayText($"Objective Failed!", pos);
        }
    }
}