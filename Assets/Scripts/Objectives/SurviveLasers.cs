using System.Collections;
using UnityEngine;

namespace Objectives {
    public class SurviveLasers : SurviveUntil {

        public float rate = .5f;
        public float laserOffsetRange = 20f;
        public int distanceAwayFromPlayer = 50;

        private Transform playerTr; 

        protected override void Start() {
            base.Start();
            playerTr = FindObjectOfType<PlayerMovement>().transform;
            StartCoroutine(SpawnLasers());
        }

        public override void Completed() {
            DisplayText($"+{scoreReward}", playerTr.transform.position);
        }

        public IEnumerator SpawnLasers()
        {
            //DisplayText("Survive The Lasers!", playerTr.transform.position, 300);
            float duration = 0;
            while (!IsCompleted())
            {
                if (duration > rate)
                {
                    SpawnLaser();
                    duration = 0;
                }
                else
                {
                    duration += Time.deltaTime;
                }
                yield return null;
            }
        }

        private void SpawnLaser()
        {
            Laser laser = Instantiate(Resources.Load<Laser>("Laser"));
            float laserWidth = Random.Range(5, 30);
            Vector3 offset = new Vector2(
                Random.Range(-laserOffsetRange, laserOffsetRange),
                Random.Range(-laserOffsetRange, laserOffsetRange)
            );
            float radians = Random.Range(0, 360) * (Mathf.PI / 180);
            Vector3 location = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * distanceAwayFromPlayer;
            // set values
            laser.transform.position = location + offset;
            laser.transform.right = playerTr.transform.position - laser.transform.position;
            laser.transform.localScale = new Vector3(1, laserWidth, 0);
        }
    }
}