﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objectives {
    public class ActivateTarget : Objective {
        private float timeLeft;
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
            timeLeft = timeToComplete;
            scoreReward = scoreReward * 3 / 2;
        }

        private void Update() {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, timeToComplete);
        }

        public override bool IsCompleted() => chargeStation?.Charged ?? false;

        public override void Cleanup() {
            chargeStation.GetComponent<Target>().Cleanup();
            Destroy(chargeStation.gameObject);
        }

        public override bool IsFailed() => timeLeft <= 0;

        public override void Completed()
        {
            Manager.AddObjective(gameObject.AddComponent<ActivateTarget>());
            GameManager.AddScore(scoreReward);
            DisplayText($"+{scoreReward}", chargeStation.transform.position);
        }

        public override void Failed()
        {
            GameManager.instance.Damage(5);
        }
    }
}