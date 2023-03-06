using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaControl: MonoBehaviour {
	// Start is called before the first frame update
	Color ActivatedColor = new(1, 1, 1, 1);
	Color UnChargedColor = new(1, 1, 1, 1);
	public bool isCharged;
	public bool isActivated;
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void Charge() {
		isCharged = true;
	}

	void Activate() {
		isActivated = true;
	}
}
