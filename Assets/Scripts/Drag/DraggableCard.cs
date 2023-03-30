using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCard : Draggable {
    public static event Action<Transform, Vector3> OnCardReturn;
    public static event Action<CardBase> OnCardUse;
    private Vector3 StartPos;
    private Vector3 Distance;
    public bool ifDrawLine;
    public override bool CanPreview { get => !isDragging && _canPreview; set => _canPreview = value; }
    private void OnMouseDrag() {
        if (canDrag) {
            isDragging = true;
            if (ifDrawLine && Input.mousePosition.y > 200) {
                EventSystem.Invoke(EventSystem.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, transform.position, Input.mousePosition));
            }
            else {
                // transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
                transform.position = Input.mousePosition - Distance;
            }
        }
    }
    
    protected override void OnMouseUp() {
        if (Input.mousePosition.y > 200 && canDrag && isAvailable) {
            if (ifDrawLine) {
                transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
                GetComponent<BattleCardManager>().Card.Use();
                EventSystem.Invoke(EventSystem.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, GetComponent<BattleCardManager>().Card));
            }
            else {
                EventSystem.Invoke(EventSystem.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, gameObject, GetComponent<BattleCardManager>().Card));
                GetComponent<BattleCardManager>().Card.Use();
            }
        }
        else {
            OnCardReturn?.Invoke(transform, StartPos);
            isDragging = false;
        }
        if (CanPreview) {
            transform.localScale = Vector3.one;
        }

    }
}
