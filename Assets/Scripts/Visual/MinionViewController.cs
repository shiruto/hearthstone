using TMPro;
using UnityEngine;

public class MinionViewController : MonoBehaviour {
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtHealth;
    public bool canPreview = true;
    public MinionLogic ML;

    public void ReadFromMinionLogic() {
        // TODO: text color
        TxtAttack.text = ML.Attack.ToString();
        TxtHealth.text = ML.Health.ToString();
    }

}
