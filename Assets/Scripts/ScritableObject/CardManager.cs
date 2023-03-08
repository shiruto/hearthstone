using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CardManager: MonoBehaviour {

	public CardAsset cardAsset; // 卡牌SO asset
	public CardManager PreviewManager; // 卡牌预览
	[Header("Text Component References")]
	public TextMeshProUGUI NameText; // 卡牌名文本
	public TextMeshProUGUI ManaCostText; // 卡牌费用文本
	public TextMeshProUGUI DescriptionText; // 卡牌描述文本
	public TextMeshProUGUI HealthText; // 卡牌生命值文本
	public TextMeshProUGUI AttackText; // 卡牌攻击文本
	[Header("GameObject References")]

	public GameObject HealthIcon;
	public GameObject AttackIcon;
	[Header("Image References")]
	public Image CardGraphicImage; // 卡牌图像
	public Image CardFaceFrameImage; // 卡牌框图像，用于区分卡牌稀有度
	public Image CardFaceGlowImage; // 卡牌正面光效
	public Image CardBackGlowImage; // 卡牌背面光效
	public Image CardElementImage; // 卡牌元素图像
	void Awake() {
		if(cardAsset != null)
			ReadCardFromAsset();
	}

	private bool canBePlayedNow = false;
	public bool CanBePlayedNow {
		get {
			return canBePlayedNow;
		}

		set {
			canBePlayedNow = value;

			CardFaceGlowImage.enabled = value;
		}
	}

	public void ReadCardFromAsset() {
		// 1) 更新卡牌信息
		//		CardFaceFrameImage.sprite = cardAsset.CardRarityImage;
		HealthIcon.SetActive(false);
		AttackIcon.SetActive(false);
		CardElementImage.sprite = cardAsset.CardImage;

		// 2) 添加卡牌名字
		NameText.text = cardAsset.name;
		// 3) 添加卡牌消耗
		ManaCostText.text = cardAsset.ManaCost.ToString();
		// 4) 添加描述
		DescriptionText.text = cardAsset.Description;
		// 5) 更换卡牌图片
		CardGraphicImage.sprite = cardAsset.CardImage;

		if(cardAsset.MaxHealth != 0) {
			// 这是一个生物
			AttackText.text = cardAsset.Attack.ToString();
			HealthText.text = cardAsset.MaxHealth.ToString();
		}

		if(PreviewManager != null) {
			// 这是一张卡牌而不是预览
			// 预览卡牌也拥有该脚本，但属性是null
			PreviewManager.cardAsset = cardAsset;
			PreviewManager.ReadCardFromAsset();
		}
	}
}