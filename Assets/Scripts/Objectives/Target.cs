using System;
using UnityEngine;

namespace Objectives {
    public class Target : MonoBehaviour {
        [SerializeField] private Sprite arrowSprite;
        [SerializeField] private GameObject pointerPrefab;
        private RectTransform pointerTransform;
        [SerializeField] private float border = 100f;
        private Vector3 pointerWorldPos;
        private object debugPos;

        private void Start() {
            var canvas = FindObjectOfType<Canvas>();
            pointerTransform = Instantiate(pointerPrefab, canvas.transform).GetComponent<RectTransform>();
        }

        private void Update() {
            var fromPos = Camera.main.transform.position;
            fromPos.z = 0;
            var targetPositionScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            if (IsOffScreen(targetPositionScreenPoint)) HandleOffscreen(targetPositionScreenPoint);
            else HandleOnScreen(targetPositionScreenPoint);

        }

        private bool IsOffScreen(Vector3 targetPositionScreenPoint) {
            var rect = pointerTransform.rect;
            debugPos = targetPositionScreenPoint.x;
            return targetPositionScreenPoint.x - (rect.width  / 2) <= 0 ||
                   targetPositionScreenPoint.x + (rect.width  / 2) >= Screen.width ||
                   targetPositionScreenPoint.y - (rect.height / 2) <= 0 ||
                   targetPositionScreenPoint.y + (rect.height / 2) >= Screen.height;
        }

        private void HandleOffscreen(Vector3 targetPositionScreenPoint) {
            pointerTransform.localPosition = targetPositionScreenPoint;
        }
        private void HandleOnScreen(Vector3 targetPositionScreenPoint) => pointerTransform.position = targetPositionScreenPoint;

        private void OnGUI() {GUILayout.Box($"{debugPos}");
        }

        public void Cleanup() {
            if (pointerTransform) Destroy(pointerTransform.gameObject);
            Destroy(gameObject);
        }
    }
}