using System;
using UnityEngine;

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
	public static event Action<Transform> OnUse;
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
		RaycastHit[] a = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
		if(Input.GetMouseButtonDown(0) && a[0].collider.transform.gameObject.name == this.name) {
			OnMouseDown();
		}
		if(isDragging) {
			var mousePos = GetMousePosition();
			transform.position = new Vector3(mousePos.x - pointerDistance.x, mousePos.y - pointerDistance.y, 0);
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
				isReturning = false;
			}
		}
	}
	void OnMouseUp() {
		if(isDragging && RectTransformUtility.RectangleContainsScreenPoint(GameObject.Find("PnlUseCard").GetComponent<RectTransform>(), Input.mousePosition)) {
			OnUse?.Invoke(GetComponent<Transform>());
			isReturning = false;
		}
		if(isDragging) {
			isDragging = false;
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