using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int rate = 1;
    public float step = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(DefaultScoreIncrement());
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText($"{GameManager.GetScore()}");
    }

    IEnumerator DefaultScoreIncrement()
    {
        float tillNextIncrement = 0;
        while (GameManager.instance.gameOver == null)
        {
            if (tillNextIncrement > step)
            {
                GameManager.AddScore(rate);
                tillNextIncrement = 0f;
            }
            else
            {
                tillNextIncrement += Time.deltaTime;
            }
            yield return null;
        }
    }
}
