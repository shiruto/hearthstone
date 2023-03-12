using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelect: MonoBehaviour {
	List<Transform> BtnList = new();
	private void Awake() {
		foreach(Transform Btn in transform) {
			BtnList.Add(Btn);
		}

	}
}
