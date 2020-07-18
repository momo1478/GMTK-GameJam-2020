using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateTarget : Objective {
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
            chargeStation.transform.SetParent(gameObject.transform);
            DisplayName = "Charge Station";

        }

        private void Update() {
            lapsedTime += Time.deltaTime;
            
            if (timeToComplete - lapsedTime <= 4 && !animating) {
                TimeOutAnimation(chargeStation.GetComponent<SpriteRenderer>());
                TimeOutAnimation(chargeStation.gameObject.GetComponent<Target>().PointerTransform.GetComponent<Image>());
                animating = true;
            }
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