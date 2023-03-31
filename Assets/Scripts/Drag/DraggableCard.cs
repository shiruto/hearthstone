using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCard : Draggable {
    private Vector3 StartPos;
    private Vector3 Distance;
    public bool ifDrawLine;
    protected override bool IsAvailable => true;
    // protected override bool IsAvailable => BattleControl.Instance.ActivePlayer == GetComponent<BattleCardManager>().Card.owner && GetComponent<BattleCardManager>().Card.CurManaCost <= BattleControl.Instance.ActivePlayer.Mana.Manas;
    public override bool CanPreview { get => !isDragging && _canPreview; set => _canPreview = value; }
    public override bool CanBeTarget => DrawTarget(GetTarget());
    private void OnMouseEnter() {
        if (CanPreview) {
            CardPreview = Instantiate(PfbCard, transform.parent.parent);
            Destroy(CardPreview.GetComponent<BoxCollider>());
            CardPreview.GetComponent<CardManager>().cardAsset = GetComponent<BattleCardManager>().Card.CA;
            CardPreview.transform.position = new Vector3(0, 100, 0) + transform.position;
            CardPreview.transform.localScale = new(2f, 2f, 2f);
            CardPreview.GetComponent<CardManager>().ReadFromAsset();
        }
    }
    private void OnMouseDown() {
        if (canDrag) {
            if (!isDragging) StartPos = transform.position;
            Debug.Log(StartPos);
            Distance = Input.mousePosition - transform.position;
        }
        if (CanPreview) {
            Debug.Log(transform.name);
            transform.localScale = 2 * Vector3.one;
            Destroy(CardPreview);
        }
    }
    protected override void OnMouseDrag() {
        if (canDrag) {
            isDragging = true;
            if (ifDrawLine && Input.mousePosition.y > 200) {
                EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, new(0, 200, 0), Input.mousePosition));
            }
            else {
                // transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
                transform.position = Input.mousePosition - Distance;
            }
        }
    }

    protected override void OnMouseUp() {
        if (Input.mousePosition.y > 200 && canDrag && IsAvailable) {
            if (ifDrawLine) {
                transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
                (GetComponent<BattleCardManager>().Card as SpellCard).Target = GetTarget();
                GetComponent<BattleCardManager>().Card.Use();
                EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, GetComponent<BattleCardManager>().Card));
            }
            else {
                EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, GetComponent<BattleCardManager>().Card));
                GetComponent<BattleCardManager>().Card.Use();
            }
        }
        else {
            EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.OnCardReturn, gameObject, Input.mousePosition, StartPos));
            isDragging = false;
        }
        if (CanPreview) {
            transform.localScale = Vector3.one;
        }
    }
    public override bool DrawTarget(ICharacter Target) {
        GameDataAsset.TargetingOptions TargetType = GetComponent<BattleCardManager>().Card.CA.TargetsType;
        bool isMinion = Target is MinionLogic;
        bool isPlayer = Target is PlayerLogic;
        PlayerLogic owner;
        if (isMinion) {
            owner = ((MinionLogic)Target).owner;
        }
        else if (isPlayer) {
            owner = (PlayerLogic)Target;
        }
        else {
            return false;
        }
        return TargetType switch {
            GameDataAsset.TargetingOptions.AllCharacters => isMinion || isPlayer,
            GameDataAsset.TargetingOptions.AllMinions => isMinion,
            GameDataAsset.TargetingOptions.EnemyCharacters => owner == BattleControl.opponent,
            GameDataAsset.TargetingOptions.EnemyMinions => owner == BattleControl.opponent && isMinion,
            GameDataAsset.TargetingOptions.YourCharacters => owner == BattleControl.you,
            GameDataAsset.TargetingOptions.YourMinions => owner == BattleControl.you && isMinion,
            _ => false,
        };
    }


}
