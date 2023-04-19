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
            CardPreview.GetComponent<CardViewController>().CA = GetComponent<BattleCardViewController>().Card.CA;
            CardPreview.transform.position = new Vector3(0, 100, 0) + transform.position;
            CardPreview.transform.localScale = new(2f, 2f, 2f);
            CardPreview.GetComponent<CardViewController>().ReadFromAsset();
        }
    }

    private void OnMouseDown() {
        if (canDrag) {
            if (!isDragging) StartPos = transform.position;
            Distance = Input.mousePosition - transform.position;
        }
        if (CanPreview) {
            transform.localScale = 2 * Vector3.one;
            Destroy(CardPreview);
        }
    }

    protected override void OnMouseDrag() {
        if (canDrag) {
            isDragging = true;
            if (ifDrawLine && Input.mousePosition.y > 300) {
                EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, new(960, 300, 0), Input.mousePosition));
            }
            else {
                EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero));
                transform.position = Input.mousePosition - Distance;
            }
        }
    }

    protected override void OnMouseUp() {
        if (Input.mousePosition.y > 300 && canDrag && IsAvailable) {
            if (ifDrawLine) {
                EventManager.Invoke(EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero));
                GetComponent<BattleCardViewController>().Card.Target = GetTarget();
                if (GetComponent<BattleCardViewController>().Card.Target != null) {
                    GetComponent<BattleCardViewController>().Card.Use();
                    EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, BattleControl.Instance.ActivePlayer, GetComponent<BattleCardViewController>().Card));
                }

            }
            else {
                EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, BattleControl.Instance.ActivePlayer, GetComponent<BattleCardViewController>().Card));
                GetComponent<BattleCardViewController>().Card.Use();
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
        GameDataAsset.TargetingOptions TargetType = GetComponent<BattleCardViewController>().Card.CA.TargetsType;
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
