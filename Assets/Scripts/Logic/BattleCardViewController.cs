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
    public Image CardGraphicImage;
    public bool IsTriggered;
    public GameObject TriggeredLight;
    public GameObject Light;
    public CardBase Card;

    private void OnEnable() {
        if (GetComponent<DraggableCard>()) {
            DraggableCardAvailabilityCheck(null);
            EventManager.AddListener(EmptyParaEvent.ManaVisualUpdate, DraggableCardAvailabilityCheck);
            EventManager.AddListener(EmptyParaEvent.FieldVisualUpdate, DraggableCardAvailabilityCheck);
            EventManager.AddListener(TurnEvent.OnTurnStart, DraggableCardAvailabilityCheck);
            EventManager.AddListener(TurnEvent.OnTurnEnd, DraggableCardAvailabilityCheck);
        }
    }

    public void ReadFromAsset() {
        NameText.text = Card.CA.name; // 添加卡牌名字
        ManaCostText.text = Card.CA.ManaCost.ToString(); // 添加卡牌消耗
        DescriptionText.text = Card.CA.Description; // 添加描述
        //TODO: CardGraphicImage.sprite = Card.CA.CardImage;
        if (Card is MinionCard or WeaponCard) {
            CardAsset CA = Card.CA;
            AttackText.text = CA.Attack.ToString();
            HealthText.text = CA.Health.ToString();
            if (CA.MinionType != MinionType.None) {
                ExInfo.transform.parent.gameObject.SetActive(true);
                ExInfo.text = CA.MinionType.ToString("G");
            }
            else {
                ExInfo.transform.parent.gameObject.SetActive(false);
            }
        }
        else if (Card is SpellCard && Card.CA.SpellSchool != SpellSchool.None) {
            ExInfo.transform.parent.gameObject.SetActive(true);
            ExInfo.text = Card.CA.SpellSchool.ToString("G");
        }
        else {
            ExInfo.transform.parent.gameObject.SetActive(false);
        }
        HealthIcon.SetActive(Card is MinionCard or WeaponCard);
        AttackIcon.SetActive(Card is MinionCard or WeaponCard);
        if (GetComponent<DraggableCard>()) {
            DraggableCardAvailabilityCheck(null);
        }
    }

    private void DraggableCardAvailabilityCheck(BaseEventArgs e) {
        Light.SetActive(Card != null && Card.CanBePlayed);
        GetComponent<DraggableCard>().ifDrawLine = Card is ITarget && (Card is not MinionCard || Card.TargetExist);
    }

    private void OnDisable() {
        if (GetComponent<DraggableCard>()) {
            EventManager.DelListener(EmptyParaEvent.ManaVisualUpdate, DraggableCardAvailabilityCheck);
            EventManager.DelListener(EmptyParaEvent.FieldVisualUpdate, DraggableCardAvailabilityCheck);
            EventManager.DelListener(TurnEvent.OnTurnStart, DraggableCardAvailabilityCheck);
            EventManager.DelListener(TurnEvent.OnTurnEnd, DraggableCardAvailabilityCheck);
        }
    }

}
