using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtStart: MonoBehaviour {
	private TxtTitleAnimationHandler EventHandler;
	private Animator anim;
	private void Awake() {
		anim = GetComponent<Animator>();
		EventHandler = GameObject.Find("TxtTitle").GetComponent<TxtTitleAnimationHandler>();
	}

	private void AnimationStart() {
		anim.SetTrigger("AnimationStart");
	}

	private void OnEnable() {
		EventHandler.OnFinish += AnimationStart;
	}

	private void OnDisable() {
		EventHandler.OnFinish -= AnimationStart;
	}

}
