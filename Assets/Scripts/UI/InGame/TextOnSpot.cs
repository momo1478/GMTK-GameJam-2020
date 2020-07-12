using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TextOnSpot : MonoBehaviour {

	public string DisplayText;
	public int DisplayPoints;
	public TextMeshProUGUI TextPrefab;
	public float Speed;
	public float DestroyAfter;
	private float Timer;

	// Use this for initialization
	void Start () {
		Timer = DestroyAfter;
		TextPrefab = GetComponentInChildren<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		if(Timer < 0) {
			Destroy(gameObject);
		}

		if(DisplayPoints > 0) {
			TextPrefab.text = "+" + DisplayPoints + "!";
		} else if(DisplayText != null) {
			TextPrefab.text = DisplayText;
		}

		if(Speed > 0) {
			transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
		}
	}
}
