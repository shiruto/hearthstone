using UnityEngine;

public class DraggableMinion : Draggable {
    public MinionLogic Minion;

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, Minion.Card).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, Minion.Card).Invoke();
    }

    private void OnMouseDown() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
        if (Minion.CanAttack) {
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, transform.position).Invoke();
            ScnBattleUI.Instance.isDragging = true;
        }
    }

    protected override void OnMouseUp() {
        if (ScnBattleUI.Instance.isDragging && Minion.ValidTarget(ScnBattleUI.Instance.TargetCharacter)) {
            Minion.AttackAgainst(ScnBattleUI.Instance.TargetCharacter);
            Minion.CanAttack = false;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate);
        }
        ScnBattleUI.Instance.isDragging = false;
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject).Invoke();
    }

    protected override void OnMouseDrag() {

    }

}
