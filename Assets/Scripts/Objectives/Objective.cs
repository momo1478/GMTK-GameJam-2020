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

        public void DisplayText(string text, Vector3 location)
        {
            GameObject gameObject = Instantiate(Resources.Load("Prefabs/TextOnSpot")) as GameObject;
            if(gameObject.GetComponent<TextOnSpot>() != null) {
                
                var tos = gameObject.GetComponent<TextOnSpot>();
                tos.DisplayText = text;
            }
            gameObject.transform.position = location;
        }
    }
}