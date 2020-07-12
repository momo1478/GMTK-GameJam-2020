using UnityEngine;

namespace Objectives {
    [RequireComponent(typeof(ObjectiveManager))]
    public abstract class Objective : MonoBehaviour {
        protected ObjectiveManager Manager;

        public int scoreReward = 100;

        public abstract bool IsCompleted();
        public abstract void Cleanup();
        public abstract bool IsFailed();

        public abstract void Completed();

        public abstract void Failed();

        public void DisplayText(string text, Vector3 location, int textSize = 180)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/TextOnSpot")) as GameObject;
            var tos = go.GetComponent<TextOnSpot>();
            tos.TextPrefab.text = text;
            tos.SetFontSize(textSize);
            go.transform.position = location;
        }
    }
}