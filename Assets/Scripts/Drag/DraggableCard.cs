using System.Collections;
using UnityEngine;

public class DraggableCard : Draggable {
    private Vector3 StartPos;
    private Vector3 Distance;
    public bool ifDrawLine;

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, GetComponent<BattleCardViewController>().Card).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<BattleCardViewController>().Card).Invoke();
    }

    private void OnMouseDown() {
        StartPos = transform.position;
        Distance = Input.mousePosition - transform.position;
        GetComponent<BoxCollider>().center = new(0, 0, -20);
        transform.localScale *= 2;
        transform.SetAsLastSibling();
        ScnBattleUI.Instance.isDragging = true;
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<BattleCardViewController>().Card).Invoke();
    }

    protected override void OnMouseDrag() {
        if (ifDrawLine && Input.mousePosition.y > 300) {
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawCardLine, gameObject, new(960, 300, 0), Input.mousePosition).Invoke();
        }
        else {
            EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero).Invoke();
            transform.position = Input.mousePosition - Distance;
        }
    }

    protected override void OnMouseUp() {
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero).Invoke();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, GetComponent<BattleCardViewController>().Card).Invoke();
        if (Input.mousePosition.y > 300 && canDrag) {
            CardBase CardUsing = GetComponent<BattleCardViewController>().Card;
            if (ifDrawLine) {
                if (ScnBattleUI.Instance.Targeting != null) {
                    (CardUsing as ITarget).Target = ScnBattleUI.Instance.Targeting;
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
            // TODO: Card Used in BattleControl, not here. So can manage the Counter trigger.
        }
        else {
            StartCoroutine(MoveTo(StartPos));
        }
        ScnBattleUI.Instance.isDragging = false;
        GetComponent<BoxCollider>().center = new(0, 0, -(gameObject.name[^1] - '0' - 1));
        transform.localScale *= 0.5f;
        transform.SetSiblingIndex(gameObject.name[^1] - '0' - 1);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate);
    }

    public IEnumerator MoveTo(Vector3 destination) { // TODO: wrong coroutine
        while ((transform.position - destination).magnitude > destination.magnitude * Time.deltaTime) {
            transform.Translate(destination * Time.deltaTime);
            yield return null;
        }
        transform.position = destination;
    }

}
