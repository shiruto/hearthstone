using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FadeAnim: MonoBehaviour {
	// Start is called before the first frame update

	TextMeshProUGUI text;
	[VectorLabels("From", "To")]
	public Vector2 TransparencyChange = new(0, 1);
	public bool isLooping = true;
	public float fadeSpeed = 0.1f;
	bool isIncresing;
	bool isWorking = true;
	public float delay = 0.0f;
	Color tempColor;
	float startTime;
	float curTime;

	void Start() {
		text = this.GetComponent<TextMeshProUGUI>();
		isIncresing = TransparencyChange.x < TransparencyChange.y;
		tempColor = text.color;
		tempColor.a = TransparencyChange.x;
		text.color = tempColor;
		startTime = Time.time;
		//Debug.Log("Start: " + text.color.a);
	}

	// Update is called once per frame
	void Update() {
		curTime = Time.time;
		if (curTime - startTime - delay > 0.01f) {
			if(isWorking) {
				text.color = ChangeTransparency(text.color);
			}
		}
		//Debug.Log("outside of func: \ntext color: " + text.color.a + "\ttemp color: " + tempColor.a);
	}

	private Color ChangeTransparency(Color color) {
		if(isIncresing) {
			color.a += fadeSpeed * Time.deltaTime;
			if(color.a >= Mathf.Max(TransparencyChange.x, TransparencyChange.y)) {
				isIncresing = false;
			}
		}
		else {
			color.a -= fadeSpeed * Time.deltaTime;
			if(color.a <= Mathf.Min(TransparencyChange.x, TransparencyChange.y)) {
				isIncresing = true;
			}
		}
		if (!isLooping && Mathf.Abs(color.a - TransparencyChange.y) < 0.01f) {
			isWorking = false;
		}
		Debug.Log("isWorking = "+ isWorking + "\tisIncresing = " + isIncresing + "\ttransparency: "+ color.a);
		return color;
	}
}
