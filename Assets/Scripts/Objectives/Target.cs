using System;
using System.Linq;
using UnityEngine;

namespace Objectives {
    public class Target : MonoBehaviour {
        [SerializeField] private GameObject pointerPrefab;
        private RectTransform pointerTransform;
        private Vector3 pointerWorldPos;

        private void Start() {
            var canvas = FindObjectsOfType<Canvas>().First(c => c.renderMode != RenderMode.WorldSpace);
            pointerTransform = Instantiate(pointerPrefab, canvas.transform).GetComponent<RectTransform>();
        }

        private void Update() {
            var fromPos = Camera.main.transform.position;
            fromPos.z = 0;
            var targetPositionScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            if (!IsOffScreen(targetPositionScreenPoint)) pointerTransform.position = new Vector3(999999, 999999, 999999);
            else pointerTransform.position = HandleOffscreen(targetPositionScreenPoint);
        }

        private bool IsOffScreen(Vector3 targetPositionScreenPoint) {
            var rect = pointerTransform.rect;
            
            return targetPositionScreenPoint.x - (rect.width / 2) <= 0 ||
                   targetPositionScreenPoint.x + (rect.width / 2) >= Screen.width ||
                   targetPositionScreenPoint.y - (rect.height / 2) <= 0 ||
                   targetPositionScreenPoint.y + (rect.height / 2) >= Screen.height;
        }


        private Vector3 HandleOffscreen(Vector3 targetPositionScreenPoint) {
            var rect = pointerTransform.rect;
            var clampedX = Mathf.Clamp(targetPositionScreenPoint.x, 0 + (rect.width / 2),
                Screen.width - (rect.width / 2));
            var clampedY = Mathf.Clamp(targetPositionScreenPoint.y, 0 + (rect.height / 2),
                Screen.height - (rect.height / 2));
            return new Vector3(clampedX, clampedY, 0);
        }

        public void Cleanup() {
            if (pointerTransform) Destroy(pointerTransform.gameObject);
            Destroy(gameObject);
        }

        private void OnDisable() {
            Cleanup();
        }
    }
}