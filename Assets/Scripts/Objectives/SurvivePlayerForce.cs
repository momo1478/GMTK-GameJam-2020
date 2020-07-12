using System.Collections;
using UnityEngine;

namespace Objectives {
    public class SurvivePlayerForce : SurviveUntil {

        public float intensity = 50f;
        public float frequency = 1f;
        public float duration = 1f;
        public Rigidbody2D player;

        private float tillNextAction = 0f;

        protected override void Start() {
            base.Start();
            player = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
            StartCoroutine(ApplyForce());
        }

        public override void Completed() {
            DisplayText($"+{scoreReward}", player.transform.position);
        }

        private IEnumerator ApplyForce()
        {
            while (true)
            {
                if (tillNextAction > frequency)
                {
                    Vector2 randomVector = new Vector2(
                        UnityEngine.Random.Range(-1.0f, 1.0f),
                        UnityEngine.Random.Range(-1.0f, 1.0f)
                    );
                    randomVector.Normalize();
                    float elapsedTime = 0f;
                    while (elapsedTime < duration)
                    {
                        player.AddForce(randomVector * intensity);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    tillNextAction = 0f;
                }
                else
                {
                    tillNextAction += Time.deltaTime;
                }
                yield return null;
            }
        }
    }
}