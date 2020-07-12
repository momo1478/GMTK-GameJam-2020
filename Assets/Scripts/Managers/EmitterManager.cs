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
        Color.red,
    };

    public Emitter.Behavior globalBehavior = Emitter.Behavior.Oscillate;

    public float globalSpeed = 5;
    public float globalRate = 50;

    [Range(25f, 1000f)]
    public float difficultyScalingConstant = 300f;

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
                emitter.projectileSpeed = globalSpeed * (1 + (GameManager.GetScore() / difficultyScalingConstant));
                emitter.spawnRate = globalRate * (1 + (GameManager.GetScore() / difficultyScalingConstant));
            }
            // set color
            GameManager.SetBackgroundColor(COLORS[UnityEngine.Random.Range(0, COLORS.Length)]);
            yield return new WaitForSeconds(5);
        }
    }

    void OnValidate()
    {
        foreach (var emitter in emitters)
        {
            emitter.projectileSpeed = globalSpeed;
            emitter.spawnRate = globalRate;
            emitter.behavior = globalBehavior;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
