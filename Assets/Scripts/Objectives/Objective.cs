using System;
using UnityEngine;

namespace Tasks {
    [RequireComponent(typeof(ObjectiveManager))]
    public abstract class Objective : MonoBehaviour {
        public abstract bool IsCompleted();
    }
}