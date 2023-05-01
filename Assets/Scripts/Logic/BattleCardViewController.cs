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
    public TextMeshProUGUI ExInfo;
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
        if (GetComponent<DraggableCard>()) {
            GetComponent<DraggableCard>().ifDrawLine = Card is ITarget;
        }
        if (Card is MinionCard) {
            CardAsset CA = Card.CA;
            AttackText.text = CA.Attack.ToString();
            HealthText.text = CA.Health.ToString();
            if (CA.MinionType != MinionType.None) {
                ExInfo.text = CA.MinionType.ToString("G");
            }
            else {
                ExInfo.transform.parent.gameObject.SetActive(false);
            }
        }
        else if (Card is SpellCard && Card.CA.SpellSchool != SpellSchool.None) {
            ExInfo.text = Card.CA.SpellSchool.ToString("G");
        }
        else {
            ExInfo.transform.parent.gameObject.SetActive(false);
        }
        HealthIcon.SetActive(Card is MinionCard);
        AttackIcon.SetActive(Card is MinionCard);
    }

}
