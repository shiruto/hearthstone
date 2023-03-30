using System;
using UnityEngine;

public abstract class Draggable : MonoBehaviour {
    public virtual bool CanPreview { get => _canPreview; set => _canPreview = value; }
    protected bool _canPreview = true;
    protected GameObject CardPreview;
    public bool canDrag = false;
    protected bool isDragging = false;
    private Vector3 StartPos;
    Vector3 Distance;
    public GameObject PfbCard;
    public virtual event Action<Vector3, Vector3> DrawLine;
    public bool ifDrawLine;
    protected bool isAvailable = true;
    protected virtual void OnMouseEnter() {
        if (CanPreview) {
            CardPreview = Instantiate(PfbCard, transform.parent.parent);
            Destroy(CardPreview.GetComponent<BoxCollider>());
            CardPreview.GetComponent<CardManager>().cardAsset = GetComponent<BattleCardManager>().Card.CA;
            CardPreview.transform.position = new Vector3(0, 100, 0) + transform.position;
            CardPreview.transform.localScale = new(2f, 2f, 2f);
            CardPreview.GetComponent<CardManager>().ReadFromAsset();
        }
    }
    protected virtual void OnMouseExit() {
        if (CanPreview) {
            Destroy(CardPreview);
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
    protected abstract void OnMouseDrag();
    protected abstract void OnMouseUp();
    public abstract bool CanBeTarget(ICharacter Target);
}