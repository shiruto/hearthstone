using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable: MonoBehaviour {
	// ��ǰ�Ƿ��϶��ÿ��Ƶı�־
	private bool isDragging = false;
	// ���϶���������ĵ�������ľ���
	private Vector3 pointerDistance = Vector3.zero;
	// ��������Z���ϵľ���
	private float ZDistance { get; set; }
	// ��ʼ����λ��
	private Vector3 startPos;
	// �Ƿ�Ҫ����ԭʼλ��
	private bool isReturning = false;
	// �ƶ��ٶ�
	private float Speed;
	// ʹ�ÿ���ί��
	public event Action<CardAsset> OnUse;
	private void Start() {
		startPos = transform.position;
	}
	void OnMouseDown() {
		startPos = transform.position;
		isDragging = true;
		ZDistance = transform.position.z - Camera.main.transform.position.z;
		pointerDistance = GetMousePosition() - startPos;
	}
	void Update() {
		if(Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition)) {
			OnMouseDown();
		}
		if(isDragging) {
			var mousePos = GetMousePosition();
			var newtransform = new Vector3(mousePos.x - pointerDistance.x, mousePos.y - pointerDistance.y, 0);
			transform.position = newtransform;
		}
		if(Input.GetMouseButtonUp(0)) {
			OnMouseUp();
		}
		if(isReturning) {
			var dif = Vector3.Magnitude(transform.position - startPos);
			Speed = GetSpeed(dif);
			transform.position = transform.position + Speed * Time.deltaTime * Vector3.Normalize(startPos - transform.position);
			if(dif < 5) {
				transform.position = startPos;
			}
		}
	}
	void OnMouseUp() {
		if(isDragging) {
			isDragging = false;
		}
		if(RectTransformUtility.RectangleContainsScreenPoint(GameObject.Find("PnlUseCard").GetComponent<RectTransform>(), Input.mousePosition)) {
			OnUse?.Invoke(GetComponent<CardManager>().cardAsset);
			isReturning = false;
		}
		else {
			isReturning = true;
		}
	}
	// ����������������
	private Vector3 GetMousePosition() {
		var screenMousePos = Input.mousePosition;
		screenMousePos.z = ZDistance;
		return screenMousePos;
	}

	private float GetSpeed(float dif) { // �ٶȺ���
		if(dif < 300) {
			return 1200;
		}
		else {
			return dif * dif / 75;
		}
	}
}