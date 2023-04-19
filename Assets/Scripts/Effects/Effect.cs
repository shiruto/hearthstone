using System;
using UnityEngine;

public abstract class Effect {
    public virtual string Name { get => "empty effect"; }

    public GameDataAsset.EffectType effectType;

    public virtual void ActivateEffect() {
        Debug.Log("No Card Effect");
    }
}
