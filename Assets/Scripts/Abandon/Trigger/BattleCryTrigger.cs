using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCryTrigger : Trigger {
    public BattleCryTrigger() {
        TriggerName = "BattleCry";
        TriggerEffects = new();
    }
    public void ActivateTrigger() {
        // Execute all effects in the list
        foreach (Effect effect in TriggerEffects) {
            effect.ActivateEffect();
        }
    }
}
