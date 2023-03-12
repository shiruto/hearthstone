using UnityEngine;
using UnityEngine.UI;

public class BtnNextPage: MonoBehaviour {
	private CardSelect Cs;
	private void Awake() {
		Cs = GameObject.Find("BgCardSelect").GetComponent<CardSelect>();
		GetComponent<Button>().onClick.AddListener(Cs.NextPage);
	}
}
