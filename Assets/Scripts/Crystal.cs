using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
	// Start is called before the first frame update
	private bool isCharged;
	private bool isActivated;
	private Image CrystalColor;
	private Color AvailableColor = new(0, 1, 1, 1);
	private Color UnAvailableColor = new(0, 0, 0, 0);
	private Color UnChargedColor = new(0, .25f, .25f, 1);

	private void Start() {
		isCharged = false;
		isActivated = false;
		CrystalColor = GetComponent<Image>();
		CrystalColor.color = UnAvailableColor;
		
	}


	internal void Charge() { //为水晶充能
		if(!isActivated) {
			Debug.Log("Trying to Charge an unActivated Mana");
			return;
		}
		else if(isCharged) {
			Debug.Log("Trying to Charge a Charged Mana");
			return;
		}
		isCharged = true;
		CrystalColor.color = AvailableColor;
	}

	public void Consume() { // 使水晶失去法力
		if(!isActivated) {
			Debug.Log("Trying to Consume an unActivated mana");
			return;
		}
		else if(!isCharged) {
			Debug.Log("Trying to consume an unCharged mana");
			return;
		}
		isCharged = false;
		CrystalColor.color = UnChargedColor;
	}

	public void Activate() { // 激活水晶
		if(isActivated) {
			Debug.Log("Trying to Activate an already Actvated Mana");
			return;
		}
		isActivated = true;
		CrystalColor.color = UnChargedColor;
	}

	public void Destroy() { // 摧毁水晶
		if(!isActivated) {
			Debug.Log("Trying to Distroy an unActivated Mana");
			return;
		}
		isActivated = false;
		isCharged = false;
		CrystalColor.color = UnAvailableColor;
	}
}
