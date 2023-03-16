using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPrevManager: MonoBehaviour {

	public CardAsset cardAsset;
	[Header("Text Component References")]
	public TextMeshProUGUI NameText;
	public TextMeshProUGUI ManaCostText;
	public TextMeshProUGUI CardNum;

	[Header("Image References")]
	public Image CardGraphicImage;

	void Awake() {
		if(cardAsset != null) {
			ReadFromAsset();
		}
	}

	public void ReadFromAsset() {
		NameText.text = cardAsset.name;
		ManaCostText.text = cardAsset.ManaCost.ToString();
	}
}