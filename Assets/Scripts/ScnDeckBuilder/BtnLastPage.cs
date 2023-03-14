using UnityEngine;
using UnityEngine.UI;

public class BtnLastPage: MonoBehaviour {
	private CardSelect Cs;
	private void Awake() {
		Cs = GameObject.Find("BgCardSelect").GetComponent<CardSelect>();
		GetComponent<Button>().onClick.AddListener(Cs.LastPage);
	}
}
