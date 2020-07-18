using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Objectives {
    [RequireComponent(typeof(ObjectiveManager))]
    public abstract class Objective : MonoBehaviour {
        protected ObjectiveManager Manager;
        public float lapsedTime = 0;
        protected bool animating;

        [SerializeField] public float timeToComplete = 6f;


        public int scoreReward = 10;
        private int justInTimeBonus = 2;
        public string DisplayName { get; protected set; } = "";

        protected virtual void Awake() {
            if (FindObjectOfType<ObjectiveStacker>()) {
                timeToComplete += FindObjectOfType<ObjectiveStacker>().objectiveAdditionalTime;
            }
        }

        public abstract bool IsCompleted();
        public abstract void Cleanup();
        public abstract bool IsFailed();

        public abstract void Completed();

        public abstract void Failed();

        protected virtual void TimeOutAnimation(SpriteRenderer sr) {
            void setAlpha(float x) => sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, x);
            float getAlpha() => sr.color.a;

            DOTween.Sequence()
                .Append(DOTween.To(getAlpha, setAlpha, 0, 1))
                .Append(DOTween.To(getAlpha, setAlpha, 1, 0.5f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, 0.5f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .5f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .5f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .25f))
                .Play();
        }

        protected virtual void TimeOutAnimation(Image image) {
            void setAlpha(float x) => image.color = new Color(image.color.r, image.color.g, image.color.b, x);
            float getAlpha() => image.color.a;
            DOTween.Sequence()
                .Append(DOTween.To(getAlpha, setAlpha, 0, 1))
                .Append(DOTween.To(getAlpha, setAlpha, 1, 0.5f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, 0.5f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .5f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .5f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 1, .25f))
                .Append(DOTween.To(getAlpha, setAlpha, 0, .25f))
                .Play();
        }
        
        public void DisplayText(string text, Vector3 location, int textSize = 180) {
            GameObject go = Instantiate(Resources.Load("Prefabs/TextOnSpot")) as GameObject;
            var tos = go.GetComponent<TextOnSpot>();
            tos.TextPrefab.text = text;
            tos.SetFontSize(textSize);
            go.transform.position = location;
        }

        protected int calculateReward => animating ? scoreReward * justInTimeBonus : scoreReward;
    }
}