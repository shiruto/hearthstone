using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinionManager : MonoBehaviour {
    public CardAsset CA;
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtHealth;
    public bool canPreview;

    private int Attack;
    private int Health;

    public void ReadFromAsset() {
        TxtAttack.text = CA.Attack.ToString();
        TxtHealth.text = CA.MaxHealth.ToString();
    }
}
