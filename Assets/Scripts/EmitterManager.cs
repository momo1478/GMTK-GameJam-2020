using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterManager : MonoBehaviour
{
    public Emitter.Behavior globalBehavior = Emitter.Behavior.Oscillate;

    public float globalSpeed = 5;
    public float globalRate = 50;

    public Emitter[] emitters;
    // Start is called before the first frame update
    void Start()
    {
        emitters = FindObjectsOfType<Emitter>();
    }

    void OnValidate()
    {
        foreach (var emitter in emitters)
        {
            emitter.speed = globalSpeed;
            emitter.spawnRate = globalRate;
            emitter.behavior = globalBehavior;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
