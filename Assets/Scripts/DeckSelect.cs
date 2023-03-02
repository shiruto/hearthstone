using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelect: MonoBehaviour {
	// Start is called before the first frame update
	private void Awake() {
		GetComponent<Button>().onClick.AddListener(SelectDeck);
	}

	void Start() {
		Application.targetFrameRate = 60;

	}

	// Update is called once per frame
	void Update() {

	}

	void SelectDeck() {
		Image DeckPrev = GetComponent<Image>();
		Transform pic = GameObject.Find("BgSelectedDeck").transform.GetChild(0).GetChild(0);
		Debug.Log("getchild result = " + pic.name + "\tDeckPrev = " + DeckPrev);
		pic.GetComponent<Image>().sprite = DeckPrev.sprite;
		pic.localPosition = new Vector3(41, -1, 0);
		pic.localScale = new Vector3(0.18f, 0.18f, 0.18f);
	}

}
