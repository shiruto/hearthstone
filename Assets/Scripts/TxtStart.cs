using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtStart: MonoBehaviour {
	private Animator anim;
	private void Awake() {
		anim = GetComponent<Animator>();
	}

	private void AnimationStart() {
		anim.SetTrigger("AnimationStart");
	}

	private void OnEnable() {
		TxtTitleAnimationHandler.OnFinish += AnimationStart;
	}

	private void OnDisable() {
		TxtTitleAnimationHandler.OnFinish -= AnimationStart;
	}

}
