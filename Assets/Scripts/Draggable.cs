using UnityEngine;

public class Draggable: MonoBehaviour {
	// 当前是否拖动该卡牌的标志
	private bool isDragging = false;
	// 从拖动对象的中心到鼠标点击的距离 
	private Vector3 pointerDistance = Vector3.zero;
	// 相机与鼠标Z轴上的距离
	private float zDistance;
	void OnMouseDown() {
		isDragging = true;
		zDistance = -Camera.main.transform.position.z + transform.position.z;
		Vector3 pointerPos = MousePos();
		Debug.Log("starting pointer position = " + pointerPos);
		pointerDistance = -transform.position + pointerPos;

	}
	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			OnMouseDown();
		}
		if(isDragging) {
			Vector3 mousePos = MousePos();
			Debug.Log(mousePos);
			transform.position = new Vector3(mousePos.x - pointerDistance.x, mousePos.y - pointerDistance.y, transform.position.z);
		}
		if (Input.GetMouseButtonUp(0)) {
			OnMouseUp();
		}
	}
	void OnMouseUp() {
		if(isDragging) {
			isDragging = false;
		}
	}
	// 返回鼠标的世界坐标
	private Vector3 MousePos() {
		var screenMousePos = Input.mousePosition;
		Debug.Log("Mouse position: " + screenMousePos);
		screenMousePos.z = zDistance;
		return Camera.main.ScreenToWorldPoint(screenMousePos) * 100;
	}

}