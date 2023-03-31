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
        Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity);
        Transform TargetTrans = hitInfo.collider.transform;
        if (TargetTrans.GetComponent<MinionManager>()) {
            Targeting = TargetTrans.GetComponent<MinionManager>().ML;
        }
        else if (TargetTrans.GetComponent<PlayerLogic>()) {
            Targeting = TargetTrans.GetComponent<PlayerLogic>();
        }
        return Targeting;
    }

}