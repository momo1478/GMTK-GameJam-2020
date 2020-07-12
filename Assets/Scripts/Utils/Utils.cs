using UnityEngine;

namespace Utils {
    public static class Utils {
        public static Vector3 RandomPositionOnBoard() => new Vector3(Random.Range(-10,10),Random.Range(-10,10),0);
    }
}