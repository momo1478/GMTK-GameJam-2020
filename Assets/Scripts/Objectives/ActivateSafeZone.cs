using System;
using Safe_Zones;
using UnityEngine;
using UnityEngine.UI;
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
            if (safeZone == null) return;
            if (safeZone.HasActivated() ) return;
            
            lapsedTime += Time.deltaTime;
            
            if (timeToComplete - lapsedTime <= 4 && !animating) {
                TimeOutAnimation(safeZone.GetComponent<SpriteRenderer>());
                TimeOutAnimation(safeZone.gameObject.GetComponent<Target>().PointerTransform.GetComponent<Image>());
                animating = true;
            }
        }

        public override bool IsCompleted()
        {
            if (safeZone == null) return false;
            return safeZone.HasActivated();
        } 

        public override void Cleanup() {
            if (safeZone == null) return;
            safeZone.Cleanup();
        }

        public override bool IsFailed() {
            return lapsedTime > timeToComplete;
        }

        public override void Completed() {
            Manager.AddObjectiveSoon(timeToComplete - lapsedTime, () => Manager.AddObjective(Manager.gameObject.AddComponent<ActivateSafeZone>()));
            GameManager.AddScore(calculateReward);
            DisplayText($"+{calculateReward}", safeZone.transform.position);
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