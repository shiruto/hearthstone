using TMPro;
using UnityEngine;

public class MinionViewController : MonoBehaviour {
    public TextMeshProUGUI TxtAttack;
    public TextMeshProUGUI TxtHealth;
    public MinionLogic ML;
    public GameObject Frozen;
    public GameObject Reborn;
    public GameObject DivineShield;
    public GameObject Immune;
    public GameObject Trigger;
    public GameObject DeathRattle;
    public GameObject Windfury;
    public GameObject LifeSteal;
    public GameObject Stealth;
    public GameObject Taunt;
    public GameObject Poisonous;
    public GameObject Elusive;
    public GameObject Light;

    private void Start() {
        EventManager.AddListener(MinionEvent.AfterMinionStatusChange, UpdateStatus);
        EventManager.AddListener(EmptyParaEvent.FieldVisualUpdate, CanAttackCheck);
    }

    public void ReadFromMinionLogic() {
        // TODO: text color
        TxtAttack.text = ML.Attack.ToString();
        TxtHealth.text = ML.Health.ToString();
        Frozen.SetActive(ML.Attributes.Contains(CharacterAttribute.Frozen));
        Reborn.SetActive(ML.Attributes.Contains(CharacterAttribute.Reborn));
        DivineShield.SetActive(ML.Attributes.Contains(CharacterAttribute.DivineShield));
        Immune.SetActive(ML.Attributes.Contains(CharacterAttribute.Immune));
        Trigger.SetActive(ML.Triggers != null && ML.Triggers.Count != 0);
        DeathRattle.SetActive(ML.DeathRattleEffects.Count != 0);
        Windfury.SetActive(ML.Attributes.Contains(CharacterAttribute.Windfury));
        LifeSteal.SetActive(ML.Attributes.Contains(CharacterAttribute.LifeSteal));
        Taunt.SetActive(ML.Attributes.Contains(CharacterAttribute.Taunt));
        Poisonous.SetActive(ML.Attributes.Contains(CharacterAttribute.Poisonous));
        Elusive.SetActive(ML.Attributes.Contains(CharacterAttribute.Elusive));
    }

    private void UpdateStatus(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion == ML) {
            ReadFromMinionLogic();
        }
        CanAttackCheck(null);
    }

    private void CanAttackCheck(BaseEventArgs e) {
        Light.SetActive(ML.CanAttack);
    }

}
