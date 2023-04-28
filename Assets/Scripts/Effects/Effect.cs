using UnityEngine;

public abstract class Effect {
    public virtual string Name => "empty effect";
    public EffectType effectType;

    public virtual void ActivateEffect() { // TODO: remove the activate?
        Debug.Log("No Card Effect");
    }

}
