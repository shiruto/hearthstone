using System.Collections;
using UnityEngine;

public class DraggableCard : Draggable {
    private Vector3 StartPos;
    private Vector3 Distance;
    private readonly Vector3 SpellTargetCardStartPos = new(960, 300, 0);
    [HideInInspector]
    public bool ifDrawLine;

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, GetComponent<BattleCardViewController>().Card).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
    }

    private void OnMouseDown() {
        ScnBattleUI.Instance.isDragging = true;
        StartPos = transform.position;
        Distance = Input.mousePosition - transform.position;
        GetComponent<BoxCollider>().center = new(0, 0, -20);
        transform.localScale *= 2;
        transform.SetAsLastSibling();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
    }

    protected override void OnMouseDrag() {
        if (ifDrawLine && Input.mousePosition.y > 300) {
            if (!LineDrawer.Instance.isDrawing)
                EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, SpellTargetCardStartPos).Invoke();
            GetComponent<CanvasGroup>().alpha = 0;
        }
        else {
            GetComponent<CanvasGroup>().alpha = 1;
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject).Invoke();
            transform.position = Input.mousePosition - Distance;
        }
    }

    protected override void OnMouseUp() {
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject).Invoke();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject).Invoke();
        if (Input.mousePosition.y > 300 && ScnBattleUI.Instance.isDragging) { // TODO: check the CanBePalyed before using
            CardBase CardUsing = GetComponent<BattleCardViewController>().Card;
            if (ifDrawLine) {
                if (ScnBattleUI.Instance.TargetCharacter != null && (CardUsing as ITarget).Match(ScnBattleUI.Instance.TargetCharacter)) {
                    (CardUsing as ITarget).Target = ScnBattleUI.Instance.TargetCharacter;
                    BattleControl.Instance.CardUsing = CardUsing;
                    EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.BeforeCardUse, gameObject, CardUsing.Owner, CardUsing).Invoke();
                    BattleControl.Instance.CardUsing?.Use();
                }
                else StartCoroutine(MoveTo(StartPos)); // target select failed.
            }
            else {
                BattleControl.Instance.CardUsing = CardUsing;
                EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.BeforeCardUse, gameObject, CardUsing.Owner, CardUsing).Invoke();
                BattleControl.Instance.CardUsing?.Use();
            }
        }
        else {
            // TODO: show the "cant use" warning
            StartCoroutine(MoveTo(StartPos));
        }
        ScnBattleUI.Instance.isDragging = false;
        GetComponent<BoxCollider>().center = new(0, 0, -(gameObject.name[^1] - '0' - 1));
        transform.localScale *= 0.5f;
        transform.SetSiblingIndex(gameObject.name[^1] - '0' - 1);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate);
    }

    private IEnumerator MoveTo(Vector3 destination) {
        float elapsedTime = 0;
        while (elapsedTime < 1f) {
            transform.position = Vector3.Lerp(transform.position, destination, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;
    }

}
