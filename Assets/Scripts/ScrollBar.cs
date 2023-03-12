using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollBar: MonoBehaviour {
	private Transform CardPanel;
	private Transform SrlBar;

	private SelectedCards SCs;
	private void Awake() {
		CardPanel = GameObject.Find("MaskSelectedCards").transform.GetChild(0);
		SrlBar = GameObject.Find("SrlBar").transform;
		SrlBar.gameObject.SetActive(false);
		SCs = CardPanel.GetComponent<SelectedCards>();
		SCs.OnCardOverLoad += OnCardOverLoadHandler;
	}

	private void OnCardOverLoadHandler() {
		SrlBar.gameObject.SetActive(true);
	}

	
}
