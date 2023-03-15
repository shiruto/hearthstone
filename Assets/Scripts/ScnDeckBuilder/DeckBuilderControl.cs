using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckBuilderControl: MonoBehaviour {
	public static bool isEditing = false;
	public static bool isSelectingClass = false;
	public static event Action<Transform> OnClassFilter;
	public static event Action<Transform> OnCardClick;
	public static event Action<Transform> OnCardPrevClick;
	public static event Action<Transform> OnDeckPrevClick;
	public static event Action OnExitEditing;
	public static event Action<string> OnClassSelect;

	RaycastHit hit = new();
	Ray ray;
	private void Awake() {
		Application.targetFrameRate = 60;
	}

	private void Start() {
		GameObject.Find("PnlCardList").SetActive(false);
		GameObject.Find("PnlClassSelect").SetActive(false);
		GameObject.Find("DeckSrlBar").SetActive(false);
	}
	private void Update() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawLine(Camera.main.transform.position, Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("UI"))) {
			//string name = hit.collider.name;
			//Debug.Log(name);
			GameObject go = hit.collider.gameObject;
			if(Input.GetMouseButtonDown(0)) {
				if(go.GetComponent<Button>()) {
					if(go.name == "BtnReturn") {
						if(isEditing) {
							isEditing = false;
							OnExitEditing?.Invoke();
						}
						else {
							SceneManager.LoadScene("ScnModeSelect");
						}
					}
					else if(Enum.IsDefined(typeof(GameDataAsset.ClassType), go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)) {
						OnClassFilter?.Invoke(go.transform);
					}
				}
				else if(isEditing && go.GetComponent<CardManager>()) {
					OnCardClick?.Invoke(go.transform);
				}
				else if(isEditing && go.GetComponent<CardPrevManager>()) {
					OnCardPrevClick?.Invoke(go.transform);
				}
				else if(!isEditing && go.GetComponent<DeckPrevManager>()) {
					isEditing = true;
					OnDeckPrevClick?.Invoke(go.transform);
				}
				else if(isSelectingClass) {
					OnClassSelect?.Invoke(go.name);
				}
			}
		}

	}
}
