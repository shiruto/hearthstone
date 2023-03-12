using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager: MonoBehaviour {

	public CardAsset cardAsset; // ����SO asset
	public CardManager PreviewManager; // ����Ԥ��
	[Header("Text Component References")]
	public TextMeshProUGUI NameText; // �������ı�
	public TextMeshProUGUI ManaCostText; // ���Ʒ����ı�
	public TextMeshProUGUI DescriptionText; // ���������ı�
	public TextMeshProUGUI HealthText; // ��������ֵ�ı�
	public TextMeshProUGUI AttackText; // ���ƹ����ı�
	[Header("GameObject References")]

	public GameObject HealthIcon;
	public GameObject AttackIcon;
	[Header("Image References")]
	public Image CardGraphicImage; // ����ͼ��
	public Image CardFaceFrameImage; // ���ƿ�ͼ���������ֿ���ϡ�ж�
	public Image CardFaceGlowImage; // ���������Ч
	public Image CardBackGlowImage; // ���Ʊ����Ч
	public Image CardElementImage; // ����Ԫ��ͼ��
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
		// 1) ���¿�����Ϣ
		//		CardFaceFrameImage.sprite = cardAsset.CardRarityImage;
		HealthIcon.SetActive(false);
		AttackIcon.SetActive(false);
		CardElementImage.sprite = cardAsset.CardImage;

		// 2) ��ӿ�������
		NameText.text = cardAsset.name;
		// 3) ��ӿ�������
		ManaCostText.text = cardAsset.ManaCost.ToString();
		// 4) �������
		DescriptionText.text = cardAsset.Description;
		// 5) ��������ͼƬ
		CardGraphicImage.sprite = cardAsset.CardImage;

		if(cardAsset.MaxHealth != 0) {
			HealthIcon.SetActive(true);
			AttackIcon.SetActive(true);
			// ����һ������
			AttackText.text = cardAsset.Attack.ToString();
			HealthText.text = cardAsset.MaxHealth.ToString();
		}

		if(PreviewManager != null) {
			// ����һ�ſ��ƶ�����Ԥ��
			// Ԥ������Ҳӵ�иýű�����������null
			PreviewManager.cardAsset = cardAsset;
			PreviewManager.ReadCardFromAsset();
		}
	}
}