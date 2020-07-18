using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateTarget : Objective {
        [SerializeField] private float maxCharge = 1.5f;
        private ChargeStation chargeStation;

        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            var objectives = FindObjectsOfType<Target>().Select(x => {
                if (x.transform != null)
                    return x.transform;
                else return null;
            }).Where(x => x !=null).ToList();
            var rndmPos = objectives.ElementAtOrDefault(Random.Range(0, objectives.Count));
            
            var spawnPos = rndmPos != null && Random.Range(0, 10) >= 5f
                ? Utils.Utils.RandomPositionNear(rndmPos.position)
                : Utils.Utils.RandomPositionOnBoard();
            chargeStation = Instantiate(Resources.Load<ChargeStation>("ChargeStation"),
                spawnPos, Quaternion.identity);
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
            Manager.AddObjectiveSoon(timeToComplete - lapsedTime, () => Manager.AddObjective(Manager.gameObject.AddComponent<ActivateTarget>()));
            GameManager.AddScore(calculateReward);
            DisplayText($"+{calculateReward}", chargeStation.transform.position);
        }


        public override void Failed() {
            GameManager.instance.Damage(2);
            RenderFailMessage();
        }

        private void RenderFailMessage() {
            var player = FindObjectOfType<PlayerMovement>();
            if (player == null) return;
            DisplayText($"Objective Failed!", player.transform.position);
        }
    }
}