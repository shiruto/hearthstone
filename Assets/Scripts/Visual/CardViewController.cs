using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {
    public CardAsset CA; // 卡牌SO asset
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

    public void ReadFromAsset() {
        NameText.text = CA.name; // 添加卡牌名字
        ManaCostText.text = CA.ManaCost.ToString(); // 添加卡牌消耗
        DescriptionText.text = CA.Description; // 添加描述
        // CardGraphicImage.sprite = Card.CA.CardImage; // 更换卡牌图片
        if (CA.cardType == GameDataAsset.CardType.Minion) {
            AttackText.text = CA.Attack.ToString();
            HealthText.text = CA.Health.ToString();
        }
        HealthIcon.SetActive(CA.cardType == GameDataAsset.CardType.Minion);
        AttackIcon.SetActive(CA.cardType == GameDataAsset.CardType.Minion);
    }

}