using System;
using UnityEngine;

public class TxtTitleAnimationHandler: MonoBehaviour {
	// ��ɶ�������ί��
	public event Action OnFinish;
	// ����
	private void AnimationFinishedTrigger() => OnFinish?.Invoke();

}
