using UnityEngine;

namespace Utils {
    public static class Utils {
        public static Vector3 RandomPositionOnBoard() => new Vector3(Random.Range(-35,35),Random.Range(-35,35),0);
    }
}