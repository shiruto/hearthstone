using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandControl: MonoBehaviour {
	List<Transform> CardTrans = new();
	//public static event Action<Transform> OnUse;
	private Vector3 pivot;
	private readonly int gap = -14;
	void Start() {
		foreach(Transform child in transform) {
			CardTrans.Add(child);
		}
		Draggable.OnUse += OnUseHandler;
		DeckControl.OnDraw += OnDrawHandler;
		pivot = Vector3.zero;
		Align();
	}

	private void OnDisable() {
		Draggable.OnUse -= OnUseHandler;
		DeckControl.OnDraw -= OnDrawHandler;
	}

	private void OnUseHandler(Transform CardTransform) { // �������ı�
		char[] CharNum = CardTransform.name.ToCharArray();
		int index = CharNum[^1] - '0';
		Debug.Log("OnUseHandler: " + " index = " + index);
		DiscardCrad(index);
		//OnUse?.Invoke(CardTransform); // ����Ч����Ч
	}

	private void OnDrawHandler(CardAsset Card) {
		Transform CardTrans = (GameObject.Instantiate(Resources.Load("Prefabs/PfbCrad"))as GameObject).transform;
		GetCard(CardTrans);
	}

	public void GetCard(Transform newCard) {
		if(CardTrans.Count >= 10) {
			Debug.Log("Too Many Cards");
			return;
		}
		CardTrans.Add(newCard);
		Align();
	}

	public void DiscardCrad(int index) {
		index -= 1;
		Debug.Log("Discarding Card :index = " + index + "\tname = " + CardTrans[index].name);
		Destroy(CardTrans[index].gameObject);
		Debug.Log(CardTrans[index].name);
		CardTrans.RemoveAt(index);
		Align();
	}

	private void Align() { // �������п��� ��Ҫʵ�ִ����������ƺ����پɿ���
		if(CardTrans.Count > 6) {
			Debug.Log(CardTrans.Count);
			for(int i = 0; i < CardTrans.Count; i++) {
				Debug.Log(CardTrans[i].name);
			}
		}
		else {
			int PanelWidth = CardTrans.Count * 100;
			Debug.Log("Align");
			for(int i = 0; i < CardTrans.Count; i++) {
				pivot.x = -PanelWidth / 2 + 57 * (i + 1) - gap * (i != 0 ? 1 : 0);
				CardTrans[i].localPosition = pivot;
				CardTrans[i].GetComponent<BoxCollider>().size += new Vector3(0, 0, i);
			}
		}
		for (int i = 0;i < CardTrans.Count; i++) {
			CardTrans[i].name = "card" + (i + 1);
		}
	}

}
