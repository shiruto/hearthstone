using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScnBattleUI: MonoBehaviour {
	RaycastHit hitInfo = new();

	private void Awake() {
		Application.targetFrameRate = 60;
	}
	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawLine(Camera.main.transform.position, Input.mousePosition, Color.yellow);
		if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
			//Debug.Log($"{hitInfo.collider.name}");
		}
	}
}
