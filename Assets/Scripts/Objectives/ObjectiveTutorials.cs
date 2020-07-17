using System;
using System.Collections;
using UnityEngine;

namespace Objectives {
    [RequireComponent(typeof(ObjectiveManager))]
    public class ObjectiveTutorials : MonoBehaviour {
        
        public enum Objectives
        {
            MoveToArea,
            ActivateTarget,
            SurviveLasers,
            ActivateSafeZone
        }
        [HideInInspector] public ObjectiveManager objectiveManager;
        private GameObject player;
        private void Start() {
            objectiveManager = GetComponent<ObjectiveManager>();
            player = FindObjectOfType<PlayerMovement>().gameObject;
            StartCoroutine(TutorializeMoveToArea());
        }

        private IEnumerator TutorializeMoveToArea() {
            yield return StartCoroutine(AddTutorialText("MOVE TO THE CIRCLE TO COMPLETE THE OBJECTIVE"));
            AddNewObjective(Objectives.MoveToArea);
        }

        private IEnumerator AddTutorialText(string s) {
            var pos = player.transform.position;
            pos.y += 5f;
            DisplayText(s, pos, 200);
            yield return new WaitForSeconds(2f);
        }
        
        public void DisplayText(string text, Vector3 location, int textSize = 180)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/TutorialText")) as GameObject;
            var tos = go.GetComponent<TextOnSpot>();
            tos.DestroyAfter = 8f;    
            tos.TextPrefab.text = text;
            tos.SetFontSize(textSize);
            go.transform.position = location;
        }
        
        void AddNewObjective(Objectives type)
        {
            switch (type)
            {
                case Objectives.MoveToArea:
                    objectiveManager.AddObjective(gameObject.AddComponent<MoveToArea>());
                    break;
                case Objectives.ActivateTarget:
                    objectiveManager.AddObjective(gameObject.AddComponent<ActivateTarget>());
                    break;
                case Objectives.SurviveLasers:
                    objectiveManager.AddObjective(gameObject.AddComponent<SurviveLasers>());
                    break;
                case Objectives.ActivateSafeZone:
                    objectiveManager.AddObjective(gameObject.AddComponent<ActivateSafeZone>());
                    break;
            }
        }
    }
}