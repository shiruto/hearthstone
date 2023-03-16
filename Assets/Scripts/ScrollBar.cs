using UnityEngine;

public class ScrollBar: MonoBehaviour {
	private Transform CardPanel;

	private SelectedCards SCs;
	private void Awake() {
		CardPanel = GameObject.Find("MaskSelectedCards").transform.GetChild(0);
		SCs = CardPanel.GetComponent<SelectedCards>();
		SCs.OnCardOverLoad += OnCardOverLoadHandler;
	}
	private void Start() {
		transform.gameObject.SetActive(false);
	}

	private void OnCardOverLoadHandler() {
		transform.gameObject.SetActive(true);
	}
}
