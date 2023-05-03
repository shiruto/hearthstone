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

    private void OnEnable() {
        CanAttackCheck(null);
        EventManager.AddListener(MinionEvent.AfterMinionStatusChange, UpdateStatus);
        EventManager.AddListener(TurnEvent.OnTurnStart, CanAttackCheck);
        EventManager.AddListener(TurnEvent.OnTurnEnd, CanAttackCheck);
    }

    public void ReadFromMinionLogic() {
        // TODO: text color
        TxtAttack.text = ML.Attack.ToString();
        TxtHealth.text = ML.Health.ToString();
        Frozen.SetActive(ML.Attributes.Contains(CharacterAttribute.Frozen));
        Windfury.SetActive(ML.Attributes.Contains(CharacterAttribute.Windfury));
        Elusive.SetActive(ML.Attributes.Contains(CharacterAttribute.Elusive));
        DivineShield.SetActive(ML.Attributes.Contains(CharacterAttribute.DivineShield));
        Immune.SetActive(ML.Attributes.Contains(CharacterAttribute.Immune));
        Reborn.SetActive(ML.Attributes.Contains(CharacterAttribute.Reborn));
        Trigger.SetActive(ML.Triggers != null && ML.Triggers.Count != 0);
        DeathRattle.SetActive(ML.DeathRattleEffects.Count != 0);
        LifeSteal.SetActive(ML.Attributes.Contains(CharacterAttribute.LifeSteal));
        Poisonous.SetActive(ML.Attributes.Contains(CharacterAttribute.Poisonous));
        Taunt.SetActive(ML.Attributes.Contains(CharacterAttribute.Taunt));
        Stealth.SetActive(ML.Attributes.Contains(CharacterAttribute.Stealth));
        CanAttackCheck(null);
    }

    private void UpdateStatus(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion == ML) {
            ReadFromMinionLogic();
        }
    }

    private void CanAttackCheck(BaseEventArgs e) {
        Light.SetActive(ML != null && ML.CanAttack);
    }

    private void OnDisable() {
        EventManager.DelListener(MinionEvent.AfterMinionStatusChange, UpdateStatus);
        EventManager.DelListener(TurnEvent.OnTurnStart, CanAttackCheck);
        EventManager.DelListener(TurnEvent.OnTurnEnd, CanAttackCheck);
    }

}
