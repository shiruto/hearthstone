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

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.PlayerVisualUpdate, PlayerVisualUpdateHandler);
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
        Debug.Log("you? " + (Player == BattleControl.you) + " " + Player.Health);
    }

    private void OnMouseDrag() {
        if (Player.CanAttack) {
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawMinionLine, gameObject, transform.position, Input.mousePosition).Invoke();
        }
    }

    private void OnMouseUp() {
        if (DrawTarget(ScnBattleUI.Instance.Targeting)) {
            EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.BeforeAttack, gameObject, Player, ScnBattleUI.Instance.Targeting).Invoke();
            Player.AttackAgainst(ScnBattleUI.Instance.Targeting);
        }
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero).Invoke();
    }

    public bool DrawTarget(ICharacter Target) {
        if (Target == null) return false;
        if (Target == Player) return false;
        if (Target is MinionCard) return (Target as MinionCard).Owner != Player;
        // 嘲讽判断
        if (BattleControl.opponent.Field.GetMinions().Exists((MinionLogic a) => a.Attributes.Contains(CharacterAttribute.Taunt)) && !(Target as MinionLogic).Attributes.Contains(CharacterAttribute.Taunt)) {
            return false;
        }
        // 潛行,免疫判断
        if (Target.Attributes.Contains(CharacterAttribute.Immune) || Target.Attributes.Contains(CharacterAttribute.Stealth)) return false;
        if (Target is MinionLogic or PlayerLogic) return true;
        return false;
    }

}