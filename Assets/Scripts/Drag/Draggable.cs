using System;
using UnityEngine;

public abstract class Draggable : MonoBehaviour {
    public virtual bool CanPreview { get => _canPreview; set => _canPreview = value; }
    protected bool _canPreview = true;
    protected virtual bool IsAvailable { get; }
    protected GameObject CardPreview;
    public bool canDrag = false;
    protected bool isDragging = false;
    public abstract bool CanBeTarget { get; }
    public GameObject PfbCard;
    Ray ray;
    protected virtual void OnMouseExit() {
        if (CanPreview) {
            Destroy(CardPreview);
        }
    }

    protected abstract void OnMouseDrag();
    protected abstract void OnMouseUp();
    public abstract bool DrawTarget(ICharacter Target);

    public ICharacter GetTarget() {
        ICharacter Targeting = null;
        Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"));
        Transform TargetTrans = hitInfo.collider.transform;
        Debug.Log(TargetTrans.name);
        if (TargetTrans.GetComponent<MinionViewController>()) {
            Targeting = TargetTrans.GetComponent<MinionViewController>().ML;
        }
        else if (TargetTrans.GetComponent<PlayerVisual>()) {
            Targeting = TargetTrans.GetComponent<PlayerVisual>().Player;
        }
        return Targeting;
    }

}