using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Objectives {
    public class MoveToArea : Objective {
        private Transform playerTr;
        public Transform targetTr { get; private set; }
        private float range = 3f;
        private float threshold;
        [SerializeField] private GameObject targetPrefab;

        private void Start() {
            Manager = GetComponent<ObjectiveManager>();
            playerTr = FindObjectOfType<PlayerMovement>().transform;
            if (targetPrefab == null) targetPrefab = Resources.Load<GameObject>("MoveToTarget");

            var objectives = FindObjectsOfType<Target>().Select(x => {
                if (x.transform != null)
                    return x.transform;
                else return null;
            }).Where(x => x !=null).ToList();
            var rndmPos = objectives.ElementAtOrDefault(Random.Range(0, objectives.Count));
            
            var spawnPos = rndmPos != null && Random.Range(0, 10) >= 5f
                ? Utils.Utils.RandomPositionNear(rndmPos.position)
                : Utils.Utils.RandomPositionOnBoard();

            targetTr = Instantiate(targetPrefab, spawnPos,
                Quaternion.identity).transform;
            targetTr.localScale *= Random.Range(range / 2f, range);
            threshold = range / 2;
            targetTr.SetParent(gameObject.transform);
            DisplayName = "Move To Area";
        }

        private void Update() {
            lapsedTime += Time.deltaTime;
            if (timeToComplete - lapsedTime <= 4 && !animating) {
                TimeOutAnimation(targetTr.GetComponent<SpriteRenderer>());
                TimeOutAnimation(targetTr.gameObject.GetComponent<Target>().PointerTransform.GetComponent<Image>());
                animating = true;
            }
        }

        public override bool IsCompleted() {
            if (targetTr == null || playerTr == null) return false;
            return Vector2.Distance(playerTr.position, targetTr.position) <= threshold;
        }

        public override void Cleanup() {
            targetTr.GetComponent<Target>().Cleanup();
            Destroy(targetTr.gameObject);
        }

        public override bool IsFailed() => lapsedTime > timeToComplete;

        public override void Completed() {
            Manager.AddObjectiveSoon(timeToComplete - lapsedTime,
                () => Manager.AddObjective(Manager.gameObject.AddComponent<MoveToArea>()));
            GameManager.AddScore(calculateReward);
            DisplayText($"+{calculateReward}", targetTr.position);
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