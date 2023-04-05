using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinionManager : MonoBehaviour {
    public CardAsset CA;
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtHealth;
    public bool canPreview = true;
    public MinionLogic ML;
    public void ReadFromMinionLogic() {
        CA = ML.ca;
        TxtAttack.text = ML.Attack.ToString();
        TxtHealth.text = ML.Health.ToString();
    }
}
