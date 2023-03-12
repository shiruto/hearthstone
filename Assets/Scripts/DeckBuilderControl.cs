using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderControl: MonoBehaviour {
	public bool isEditing = true;
	public static event Action<Transform> OnBtnClick;
	public static event Action<Transform> OnCardClick;
	public static event Action<Transform> OnCardPrevClick;
	public static event Action<Transform> OnDeckPrevClick;
	RaycastHit hit = new();
	Ray ray;

	private void Start() {
		Application.targetFrameRate = 60;
		isEditing = true;
	}
	private void Update() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawLine(Camera.main.transform.position, Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("UI"))) {
			string name = hit.collider.name;
			Debug.Log(name);
			if(Input.GetMouseButtonDown(0)) {
				if(hit.collider.gameObject.GetComponent<Button>()) {
					OnBtnClick?.Invoke(hit.collider.gameObject.transform);
				}
				else if (isEditing && hit.collider.gameObject.GetComponent<CardManager>()) {
					OnCardClick?.Invoke(hit.collider.gameObject.transform);
				}
				else if (isEditing && hit.collider.gameObject.GetComponent<CardPrevManager>()) {
					OnCardPrevClick?.Invoke(hit.collider.gameObject.transform);
				}
				else if (!isEditing && hit.collider.gameObject.GetComponent<DeckPrevManager>()) {
					OnDeckPrevClick?.Invoke(hit.collider.gameObject.transform);
				}
			}
		}
		
	}
}
