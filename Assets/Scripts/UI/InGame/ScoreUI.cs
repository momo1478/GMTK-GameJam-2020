using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float score = 0;
    public int rate = 1;
    public float step = 0.2f;
    private float tillNextIncrement = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tillNextIncrement > step)
        {
            AddScore(rate);
            tillNextIncrement = 0f;
        }
        else
        {
            tillNextIncrement += Time.deltaTime;
        }
    }

    public void AddScore(int rate)
    {
        score += rate;
        text.SetText($"{score:N0}");
    }

    public int GetScore()
    {
        return (int) score;
    }
}
