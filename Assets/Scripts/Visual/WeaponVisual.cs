using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVisual : MonoBehaviour {
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtDurability;
    public Image WeaponPic;
    public WeaponCard Weapon;

    public void ReadFromCard() {
        TxtAttack.text = Weapon.Attack + "";
        TxtDurability.text = Weapon.Health + "";
    }
}