using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
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
    private bool canBePlayedNow = false;

    void Awake() {
        if (cardAsset != null)
            ReadFromAsset();
    }


    public bool CanBePlayedNow {
        get {
            return canBePlayedNow;
        }

        set {
            canBePlayedNow = value;

            CardFaceGlowImage.enabled = value;
        }
    }
    public void ReadFromAsset() {
        HealthIcon.SetActive(false);
        AttackIcon.SetActive(false);
        CardElementImage.sprite = cardAsset.CardImage;
        NameText.text = cardAsset.name; // 添加卡牌名字
        ManaCostText.text = cardAsset.ManaCost.ToString(); // 添加卡牌消耗
        DescriptionText.text = cardAsset.Description; // 添加描述
        CardGraphicImage.sprite = cardAsset.CardImage; // 更换卡牌图片
        if (cardAsset.MaxHealth != 0) {
            HealthIcon.SetActive(true);
            AttackIcon.SetActive(true);
            AttackText.text = cardAsset.Attack.ToString();
            HealthText.text = cardAsset.MaxHealth.ToString();
        }
    }

}