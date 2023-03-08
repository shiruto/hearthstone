using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl: MonoBehaviour {
	// Start is called before the first frame update

	private int CardNum;
	List<CardManager> cards;
	List<Transform> CardTrans;

	void Start() {
		CardTrans = new List<Transform>(GetComponentsInChildren<Transform>());
		CardTrans.RemoveAt(0);
		for(; CardNum < CardTrans.Capacity; CardNum++) {
			cards.Add(CardTrans[CardNum].GetComponent<CardManager>());
		}
	}

	public void GetCard(Transform newCard) {
		cards.Add(newCard.GetComponent<CardManager>());
		CardNum++;
		Align();
	}

	public void DiscardCrad(int index) {
		cards.RemoveAt(index);
		CardNum--;
		Align();
	}



	private void Align() {
		if(CardNum > 3) {

		}
		else {

		}
	}
}
