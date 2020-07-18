using UnityEngine;

namespace Utils {
    public static class Utils {
        public static Vector3 RandomPositionOnBoard() => new Vector3(Random.Range(-35,35),Random.Range(-35,35),0);

        public static Vector3 RandomPositionNear(Vector3 objPosition) {
            var x = Mathf.Clamp(objPosition.x + Random.Range(-10,10), -35, 35);
            var y = Mathf.Clamp(objPosition.y + Random.Range(-10,10), -35, 35);
            return new Vector3(x,y,0);
        }
    }
}