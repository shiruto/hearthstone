using UnityEngine;

public class DraggableMinion : Draggable {
    public MinionLogic Minion;

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, GetComponent<MinionViewController>().ML.Card).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<MinionViewController>().ML.Card).Invoke();
    }

    protected override void OnMouseDrag() {
        if (Minion.CanAttack) {
            Debug.Log("Dragging");
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawMinionLine, gameObject, transform.position, Input.mousePosition).Invoke();
        }
    }

    private void OnMouseDown() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<MinionViewController>().ML.Card).Invoke();
    }

    protected override void OnMouseUp() {
        if (DrawTarget(ScnBattleUI.Instance.Targeting)) {
            EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.BeforeAttack, gameObject, GetComponent<MinionViewController>().ML, ScnBattleUI.Instance.Targeting).Invoke();
            GetComponent<MinionViewController>().ML.AttackAgainst(ScnBattleUI.Instance.Targeting);
        }
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<MinionViewController>().ML.Card).Invoke();
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero).Invoke();
    }

    public bool DrawTarget(ICharacter Target) {
        if (Target == null) return false;
        MinionLogic ml = GetComponent<MinionViewController>().ML;
        // 召唤随从的第一回合 非冲锋随从无法攻击英雄 非突袭随从无法攻击
        if (ml.NewSummoned && (!ml.Attributes.Contains(CharacterAttribute.Rush) || !ml.Attributes.Contains(CharacterAttribute.Charge))) {
            return false;
        }
        else if (ml.Attributes.Contains(CharacterAttribute.Rush)) {
            return Target is MinionLogic;
        }
        else if (ml.Attributes.Contains(CharacterAttribute.Charge)) {
            return true;
        }
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
