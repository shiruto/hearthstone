using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnEndTurn: MonoBehaviour {
	// Start is called before the first frame update
	TextMeshProUGUI TxtOpponentsTurn;
	TextMeshProUGUI TxtEndTurn;
	void Start() {
		GetComponent<Button>().onClick.AddListener(OnButtonClick);
		TxtOpponentsTurn = GameObject.Find("TxtOpponentsTurn").GetComponent<TextMeshProUGUI>();
		TxtEndTurn = GameObject.Find("TxtEndTurn").GetComponent<TextMeshProUGUI>();
	}

	Color ChangeTransparency(Color color, float Transparency) {
		color.a = Transparency;
		return color;
	}
	void OnButtonClick() {
		TxtOpponentsTurn.color = ChangeTransparency(TxtOpponentsTurn.color, 1);
		TxtEndTurn.color = ChangeTransparency (TxtEndTurn.color, 0);
		Debug.Log("Click!" + "back color = " + TxtOpponentsTurn.color + "\tfront color = " + TxtEndTurn.color);
	}
	
}
