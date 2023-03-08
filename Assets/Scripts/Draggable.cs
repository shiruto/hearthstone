using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable: MonoBehaviour {
	// 当前是否拖动该卡牌的标志
	private bool isDragging = false;
	// 从拖动对象的中心到鼠标点击的距离
	private Vector3 pointerDistance = Vector3.zero;
	// 相机与鼠标Z轴上的距离
	private float ZDistance { get; set; }
	// 起始卡牌位置
	private Vector3 startPos;
	// 是否要返回原始位置
	private bool isReturning = false;
	// 移动速度
	private float Speed;
	// 使用卡牌委托
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
	// 返回鼠标的世界坐标
	private Vector3 GetMousePosition() {
		var screenMousePos = Input.mousePosition;
		screenMousePos.z = ZDistance;
		return screenMousePos;
	}

	private float GetSpeed(float dif) { // 速度函数
		if(dif < 300) {
			return 1200;
		}
		else {
			return dif * dif / 75;
		}
	}
}