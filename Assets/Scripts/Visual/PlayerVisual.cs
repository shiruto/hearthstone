using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour {
    public PlayerLogic Player;
    public Image PlayerImage; // TODO: img
    // TODO: windfury stealth... effect image
    public GameObject Windfury;
    public GameObject Frozen;
    public GameObject Stealth;
    public GameObject DivineShield;
    public GameObject Immune;
    public GameObject Elusive;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Attack;

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.PlayerVisualUpdate, PlayerVisualUpdateHandler);
        EventManager.AddListener(EmptyParaEvent.WeaponVisualUpdate, PlayerVisualUpdateHandler);
    }

    public void ReadFromLogic() {
        PlayerVisualUpdateHandler(null);
        // TODO: add the hero profile and skill profile depending on the playerlogic

    }

    private void PlayerVisualUpdateHandler(BaseEventArgs e) {
        Health.text = Player.Health + "";
        Attack.text = Player.Attack + "";
        Armor.text = Player.Armor + "";
        Armor.transform.parent.gameObject.SetActive(Armor.text != "0");
        Attack.transform.parent.gameObject.SetActive(Attack.text != "0");
        Frozen.SetActive(Player.Attributes.Contains(CharacterAttribute.Frozen));
        Windfury.SetActive(Player.Attributes.Contains(CharacterAttribute.Windfury));
        Elusive.SetActive(Player.Attributes.Contains(CharacterAttribute.Elusive));
        DivineShield.SetActive(Player.Attributes.Contains(CharacterAttribute.DivineShield));
        Immune.SetActive(Player.Attributes.Contains(CharacterAttribute.Immune));
        Stealth.SetActive(Player.Attributes.Contains(CharacterAttribute.Stealth));
        // Debug.Log("you? " + (Player == BattleControl.you) + " " + Player.Health);
    }

    private void OnMouseUp() {
        if (ScnBattleUI.Instance.isDragging && Player.CanAttack && ValidTarget(ScnBattleUI.Instance.TargetCharacter)) {
            EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.BeforeAttack, gameObject, Player, ScnBattleUI.Instance.TargetCharacter).Invoke();
            Player.AttackAgainst(ScnBattleUI.Instance.TargetCharacter);
            Player.CanAttack = false;
        }
        ScnBattleUI.Instance.isDragging = false;
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject).Invoke();
    }

    private void OnMouseDown() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
        if (Player.CanAttack) {
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, transform.position).Invoke();
            ScnBattleUI.Instance.isDragging = true;
        }
    }

    public bool ValidTarget(ICharacter Target) {
        if (Target == null)
            return false;
        if (!Logic.IsEnemy(Player, Target))
            return false;
        if (Target.Attributes != null) {
            if (Target.Attributes.Contains(CharacterAttribute.Immune))
                return false;
            if (Target.Attributes.Contains(CharacterAttribute.Stealth))
                return false;
            if (BattleControl.GetEnemy(Player).Field.HaveTaunt && !Logic.CanTaunt(Target as MinionLogic))
                return false;
        }
        return true;
    }

}