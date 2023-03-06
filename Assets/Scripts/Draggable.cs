using UnityEngine;

public class Draggable: MonoBehaviour {
	// ��ǰ�Ƿ��϶��ÿ��Ƶı�־
	private bool isDragging = false;
	// ���϶���������ĵ�������ľ��� 
	private Vector3 pointerDistance = Vector3.zero;
	// ��������Z���ϵľ���
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
	// ����������������
	private Vector3 MousePos() {
		var screenMousePos = Input.mousePosition;
		Debug.Log("Mouse position: " + screenMousePos);
		screenMousePos.z = zDistance;
		return Camera.main.ScreenToWorldPoint(screenMousePos) * 100;
	}

}