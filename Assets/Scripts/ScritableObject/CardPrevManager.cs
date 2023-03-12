using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPrevManager: MonoBehaviour {

	public CardAsset cardAsset; // ����SO asset
	public CardManager PreviewManager; // ����Ԥ��
	[Header("Text Component References")]
	public TextMeshProUGUI NameText; // �������ı�
	public TextMeshProUGUI ManaCostText; // ���Ʒ����ı�
	public TextMeshProUGUI DescriptionText; // ���������ı�
	public TextMeshProUGUI HealthText; // ��������ֵ�ı�
	public TextMeshProUGUI AttackText; // ���ƹ����ı�
	public TextMeshProUGUI CardNum;
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

	public void ReadCardFromAsset() {
		// 2) ��ӿ�������
		NameText.text = cardAsset.name;
		// 3) ��ӿ�������
		ManaCostText.text = cardAsset.ManaCost.ToString();
		// 5) ��������ͼƬ
		//CardGraphicImage.sprite = cardAsset.CardImage;

		if(PreviewManager != null) {
			// ����һ�ſ��ƶ�����Ԥ��
			// Ԥ������Ҳӵ�иýű�����������null
			PreviewManager.cardAsset = cardAsset;
			PreviewManager.ReadCardFromAsset();
		}
	}
}