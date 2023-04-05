using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour {
    public PlayerLogic Player;
    public Image PlayerImage; // TODO: img
    // TODO: windfury stealth... effect image
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Attack;

    public void ReadFromLogic() {
        Health.text = Player.Health + "";
        Attack.text = Player.Attack + "";
        Armor.text = Player.Armor + "";
        // TODO: add the hero profile and skill profile depending on the playerlogic
        Armor.transform.parent.gameObject.SetActive(Armor.text != "0");
        Attack.transform.parent.gameObject.SetActive(Attack.text != "0");
    }

}