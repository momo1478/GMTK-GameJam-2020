using System;
using System.Linq;
using Safe_Zones;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateSafeZone : Objective {
        private SafeZone safeZone;
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
            
            safeZone = Instantiate(Resources.Load<SafeZone>("SafeZone/SafeZone"), gameObject.transform, true);
            safeZone.transform.position = spawnPos;
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
            GameManager.instance.Damage(2);
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