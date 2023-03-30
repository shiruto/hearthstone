using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
    public virtual string Name { get => "empty effect"; }
    public virtual void ActivateEffect() {
        Debug.Log("No Card Effect");
    }
}
