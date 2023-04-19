using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardViewController : MonoBehaviour {
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
    public Image CardFaceGlowImage; // 卡牌正面光效
    public CardBase Card;

    public void ReadFromAsset() {
        NameText.text = Card.CA.name; // 添加卡牌名字
        ManaCostText.text = Card.CA.ManaCost.ToString(); // 添加卡牌消耗
        DescriptionText.text = Card.CA.Description; // 添加描述
        //TODO: CardGraphicImage.sprite = Card.CA.CardImage;
        if (Card is SpellCard && GetComponent<DraggableCard>()) {
            GetComponent<DraggableCard>().ifDrawLine = (Card as SpellCard).ifDrawLine;
        }
        if (Card is MinionCard) {
            CardAsset Card = this.Card.CA;
            AttackText.text = Card.Attack.ToString();
            HealthText.text = Card.Health.ToString();
        }
        HealthIcon.SetActive(Card is MinionCard);
        AttackIcon.SetActive(Card is MinionCard);
    }

}
