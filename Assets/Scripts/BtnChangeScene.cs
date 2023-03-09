using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnChangeScene: MonoBehaviour {
	private Button Btn;
	private void Awake() {
		Btn = GetComponent<Button>();
	}

	private void OnEnable() {
		TxtTitleAnimationHandler.OnFinish += SetInteractable;
	}

	private void OnDisable() {
		TxtTitleAnimationHandler.OnFinish -= SetInteractable;
	}

	private void SetInteractable() {
		Btn.interactable = true;
	}
}
