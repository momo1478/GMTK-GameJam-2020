using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class TextOnSpot : MonoBehaviour {

	public string DisplayText;
	public int DisplayPoints;
	public TextMeshProUGUI TextPrefab;
	public float Speed;
	public float DestroyAfter;
	private float Timer;

	private int textSize = 180;

	// Use this for initialization
	void Start () {
		Timer = DestroyAfter;
	}
	
	// Update is called once per frame
	void Update () {
		TextPrefab.fontSize = textSize;
		Timer -= Time.deltaTime;
		if(Timer < 0) {
			Destroy(gameObject);
		}

		if(Speed > 0) {
			transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
		}
	}

    public void SetFontSize(int textSize)
    {
        this.textSize = textSize;
    }
}
