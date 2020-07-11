using System;
using UnityEngine;

namespace Objectives {
    public class Target : MonoBehaviour {
        [SerializeField] private Sprite arrowSprite;
        [SerializeField] private GameObject pointerPrefab;
        private RectTransform pointerTransform;
        private Vector3 pointerWorldPos;
        private object debugPos;
        private object debugPos2;

        private void Start() {
            var canvas = FindObjectOfType<Canvas>();
            pointerTransform = Instantiate(pointerPrefab, canvas.transform).GetComponent<RectTransform>();
        }

        private void Update() {
            var fromPos = Camera.main.transform.position;
            fromPos.z = 0;
            var targetPositionScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            pointerTransform.position  = HandleOffscreen(targetPositionScreenPoint);
        }

        private Vector3 HandleOffscreen(Vector3 targetPositionScreenPoint) {
            var rect = pointerTransform.rect;
            var clampedX = Mathf.Clamp(targetPositionScreenPoint.x, 0 + (rect.width / 2), Screen.width - (rect.width / 2));
            var clampedY = Mathf.Clamp(targetPositionScreenPoint.y, 0 + (rect.height / 2), Screen.height - (rect.height / 2));
            return new Vector3(clampedX, clampedY, 0);
        }
        
        public void Cleanup() {
            if (pointerTransform) Destroy(pointerTransform.gameObject);
            Destroy(gameObject);
        }
    }
}