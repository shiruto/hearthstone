using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {
    public CardAsset CA;
    [Header("Text Component References")]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ManaCostText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI ExInfo;
    [Header("GameObject References")]
    public GameObject HealthIcon;
    public GameObject AttackIcon;
    [Header("Image References")]
    public Image CardGraphicImage;
    public Image CardFaceFrameImage;
    public Image CardFaceGlowImage;

    public void ReadFromAsset() {
        NameText.text = CA.name;
        ManaCostText.text = CA.ManaCost.ToString();
        DescriptionText.text = CA.Description;
        // CardGraphicImage.sprite = Card.CA.CardImage;
        if (CA.cardType == CardType.Minion) {
            AttackText.text = CA.Attack.ToString();
            HealthText.text = CA.Health.ToString();
            if (CA.MinionType != MinionType.None) {
                ExInfo.text = CA.MinionType.ToString("G");
            }
            else {
                ExInfo.transform.parent.gameObject.SetActive(false);
            }
        }
        else if (CA.cardType == CardType.Spell) {
            if (CA.SpellSchool != SpellSchool.None) {
                ExInfo.text = CA.SpellSchool.ToString("G");
            }
            else {
                ExInfo.transform.parent.gameObject.SetActive(false);
            }
        }
        else {
            ExInfo.transform.parent.gameObject.SetActive(false);
        }
        HealthIcon.SetActive(CA.cardType == CardType.Minion);
        AttackIcon.SetActive(CA.cardType == CardType.Minion);
    }

}