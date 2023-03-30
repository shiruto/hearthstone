using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DraggingAction : MonoBehaviour {
    public abstract void OnStartDrag();

    public abstract void OnEndDrag();

    public abstract void OnDraggingInUpdate();

    // public virtual bool CanDrag {
    //     // get {
    //     //     // return BattleControl.Instance().CanControlThisPlayer(PlayerOwner);
    //     // }
    // }

    protected virtual PlayerLogic PlayerOwner {
        get {
            if (PlayerOwner.isEnemy)
                return BattleControl.opponent;
            else if (tag.Contains("Top"))
                return BattleControl.you;
            else {
                Debug.LogError("Untagged Card or creature " + transform.parent.name);
                return null;
            }
        }
    }

    protected abstract bool DragSuccessful();
}
