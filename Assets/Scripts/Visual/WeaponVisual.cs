using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVisual : MonoBehaviour {
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtDurability;
    public GameObject WeaponContent;
    public GameObject AttackIcon;
    public Image WeaponPic;
    public WeaponLogic WL;
    public GameObject Weapon;

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.WeaponVisualUpdate, VisualUpdate);
    }

    public void ReadFromCard() {
        TxtAttack.text = WL.Attack + "";
        TxtDurability.text = WL.Health + "";
    }

    private void VisualUpdate(BaseEventArgs e) {
        if (WL.Card == null) {
            Weapon.SetActive(false);
        }
        else {
            Weapon.SetActive(true);
            WeaponContent.SetActive(!WL.isClosed);
            AttackIcon.SetActive(!WL.isClosed);
            ReadFromCard();
        }
    }

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, WL.Card).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, WL.Card).Invoke();
    }

}