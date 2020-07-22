using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterManager : MonoBehaviour
{
    public static Color[] COLORS = new Color[] {
        new Color(.0634f,0.38744f,.7075472f), // blue
        new Color(.2916f,.6509f,.3f, 1), // green
        new Color(.8301f,.2310f,.6252f, 1), // purple
        new Color(.701f,.045f,.045f, 1), // red
    };

    public Emitter.Behavior globalBehavior = Emitter.Behavior.Oscillate;

    [Range(3f, 8f)]
    public float globalSpeed;
    [Range(0.75f, 1.25f)]
    public float globalRate;
    [Range(1f, 3f)] 
    public float globalPulseSize;
    [Range(15f, 25f)]
    public float maxGlobalSpeed;
    [Range(2.75f, 3.5f)]
    public float maxGlobalRate;
    [Range(5f, 10f)]
    public float maxPulseSize;
    [Range(130f, 500f)]
    public float maxDifficultyTime;

    // [Range(25f, 1000f)]
    // public float difficultyScalingConstant = 500f;

    public Emitter[] emitters;

    // Start is called before the first frame update
    void Start()
    {
        emitters = FindObjectsOfType<Emitter>();
        StartCoroutine(RandomizeEmitterBehavior());
    }
    
    

    private IEnumerator RandomizeEmitterBehavior()
    {
        while(true)
        {
            Array values = Emitter.Behavior.GetValues(typeof(Emitter.Behavior));
            Emitter.Behavior randomBehavior = (Emitter.Behavior)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            
            foreach (var emitter in emitters)
            {
                emitter.behavior = randomBehavior;
                emitter.projectileSpeed = Mathf.Lerp(globalSpeed, maxGlobalSpeed, GetDifficulty());
                emitter.spawnRate = Mathf.Lerp(globalRate, maxGlobalRate, GetDifficulty());
                emitter.pulseSize = Mathf.Lerp(globalPulseSize, maxPulseSize, GetDifficulty());
            }
            // set color
            GameManager.SetBackgroundColor(COLORS[UnityEngine.Random.Range(0, COLORS.Length)]);
            yield return new WaitForSeconds(5);
        }
    }

    private float GetDifficulty() => Mathf.Clamp01((Time.time) / maxDifficultyTime);

    void OnValidate()
    {
        foreach (var emitter in emitters)
        {
            emitter.projectileSpeed = globalSpeed;
            emitter.spawnRate = globalRate;
            emitter.behavior = globalBehavior;
        }
    }
}
