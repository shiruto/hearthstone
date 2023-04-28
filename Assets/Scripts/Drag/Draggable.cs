using System;
using UnityEngine;

public abstract class Draggable : MonoBehaviour {
    public bool canDrag = false;
    Ray ray;
    protected abstract void OnMouseDrag();
    protected abstract void OnMouseUp();

    public ICharacter GetTarget() {
        ICharacter Targeting = null;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            Transform TargetTrans = hitInfo.collider.transform;
            Debug.Log(TargetTrans.name);
            if (TargetTrans.GetComponent<MinionViewController>()) {
                Targeting = TargetTrans.GetComponent<MinionViewController>().ML;
            }
            else if (TargetTrans.GetComponent<PlayerVisual>()) {
                Targeting = TargetTrans.GetComponent<PlayerVisual>().Player;
            }
        }
        return Targeting;
    }

}