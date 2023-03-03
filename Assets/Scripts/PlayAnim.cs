using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnim: MonoBehaviour {
	// Start is called before the first frame update
	GameObject TxtStart;
	GameObject Btn;
	bool isDetecting = true;
	AnimatorStateInfo stateInfo;
	private void Awake() {
		TxtStart = GameObject.Find("TxtStart");
		Btn = GameObject.Find("Button");
		
	}
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if(isDetecting) {
			stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
			if(stateInfo.IsName("AnimTxtTitle") && (stateInfo.normalizedTime >= 1.0f)) {
				TxtStart.GetComponent<Animator>().SetTrigger("AnimationStart");
				Btn.GetComponent<Button>().interactable = true;
				isDetecting = false;
			}
		}
	}

}
