using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelect: MonoBehaviour {
	// Start is called before the first frame update

	public GameObject replaceGameObject;
	private void Awake() {
		GetComponent<Button>().onClick.AddListener(SelectDeck);
	}

	void SelectDeck() {
		//Debug.Log("find = " + GameObject.Find("PicDeckBoarder"));
		Image DeckPrev = GameObject.Find("PicDeckBoarder").transform.GetChild(0).GetComponent<Image>();
		if(replaceGameObject != null) {
			Transform pic = GameObject.Find(replaceGameObject.name).transform.GetChild(0).GetChild(0);
			//Debug.Log("getchild result = " + pic.name + "\tDeckPrev = " + DeckPrev);
			pic.GetComponent<Image>().sprite = DeckPrev.sprite;
			pic.localPosition = new Vector3(41, -1, 0);
			pic.localScale = new Vector3(0.18f, 0.18f, 0.18f);
		}

	}

}
