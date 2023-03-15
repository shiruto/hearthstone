using UnityEngine;

public class SelectedDeck: MonoBehaviour {
	private Transform BtnDelete;

	private void Awake() {
		BtnDelete = transform.Find("BtnDeleteDeck");
		BtnDelete.gameObject.SetActive(false);
	}
	private void OnMouseEnter() {
		BtnDelete.gameObject.SetActive(true);
	}

	private void OnMouseExit() {
		BtnDelete.gameObject.SetActive(false);
	}
}
