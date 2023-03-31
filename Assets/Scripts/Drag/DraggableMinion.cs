using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class DraggableMinion : Draggable {
    public override bool CanBeTarget => DrawTarget(GetTarget());
    // protected override bool IsAvailable {
    //     get => GetComponent<MinionManager>().ML.CanAttack;
    // }
    protected override bool IsAvailable => true;
    private void OnMouseEnter() {
        if (CanPreview) {
            CardPreview = Instantiate(PfbCard, transform.parent.parent);
            Destroy(CardPreview.GetComponent<BoxCollider>());
            CardPreview.GetComponent<CardManager>().cardAsset = GetComponent<MinionManager>().CA;
            CardPreview.transform.position = new Vector3(0, 100, 0) + transform.position;
            CardPreview.transform.localScale = new(2f, 2f, 2f);
            CardPreview.GetComponent<CardManager>().ReadFromAsset();
        }
    }
    protected override void OnMouseDrag() {
        if (IsAvailable) {
            Debug.Log("Dragging");
            EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, transform.position, Input.mousePosition));
        }
    }
    private void OnMouseDown() {
        if (CanPreview) {
            Destroy(CardPreview);
        }
    }
    protected override void OnMouseUp() {
        ICharacter _target = GetTarget();
        if (DrawTarget(_target)) {
            GetComponent<MinionManager>().ML.AttackAgainst(_target);
        }
        transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
    }

    public override bool DrawTarget(ICharacter Target) {
        if (Target == null) return false;
        MinionLogic ml = GetComponent<MinionManager>().ML;
        // 召唤随从的第一回合 非冲锋随从无法攻击英雄 非突袭随从无法攻击
        if (ml.NewSummoned && (!ml.isRush || !ml.isCharge)) {
            return false;
        }
        else if (ml.isRush) {
            return Target is MinionLogic;
        }
        else if (ml.isCharge) {
            return true;
        }
        // 嘲讽判断
        if (BattleControl.opponent.Field.GetMinions().Exists((MinionLogic a) => a.isTaunt) && !(Target as MinionLogic).isTaunt) {
            return false;
        }
        // 潛行,免疫判断
        if (Target.IsImmune || Target.IsStealth) return false;
        if (Target is MinionLogic or PlayerLogic) return true;
        return false;
    }
}
