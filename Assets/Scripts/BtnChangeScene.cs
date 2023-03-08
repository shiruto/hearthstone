using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnChangeScene: MonoBehaviour {
	private TxtTitleAnimationHandler EventHandler;
	private Button Btn;
	private void Awake() {
		EventHandler = GameObject.Find("TxtTitle").GetComponent<TxtTitleAnimationHandler>();
		Btn = GetComponent<Button>();
	}

	private void OnEnable() {
		EventHandler.OnFinish += SetInteractable;
	}

	private void OnDisable() {
		EventHandler.OnFinish -= SetInteractable;
	}

	private void SetInteractable() {
		Btn.interactable = true;
	}
}
