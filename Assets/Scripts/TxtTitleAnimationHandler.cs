using System;
using UnityEngine;

public class TxtTitleAnimationHandler: MonoBehaviour {
	// 完成动画播放委托
	public event Action OnFinish;
	// 触发
	private void AnimationFinishedTrigger() => OnFinish?.Invoke();

}
