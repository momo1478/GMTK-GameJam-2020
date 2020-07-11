using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public enum Effect
    {
        RandomForce
    }
    public float intensity = 50f;
    public float frequency = 1f;
    public float duration = 1f;
    public Rigidbody2D player;

    private float tillNextAction = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tillNextAction > frequency)
        {
            ApplyEffect();
            tillNextAction = 0f;
        }
        else
        {
            tillNextAction += Time.deltaTime;
        }
    }

    private void ApplyEffect()
    {
        // Apply random force
        StartCoroutine(ApplyForce());
        
        
    }

    private IEnumerator ApplyForce()
    {
        Vector2 randomVector = new Vector2(
            UnityEngine.Random.Range(-1.0f, 1.0f),
            UnityEngine.Random.Range(-1.0f, 1.0f)
        );
        randomVector.Normalize();
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            player.AddForce(randomVector * intensity);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
