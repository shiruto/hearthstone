using System.Collections.Generic;
using UnityEngine;

public class SkillCard : CardBase {

    public SkillCard(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        Debug.Log("Use Hero Power");
        if (ManaCost > 0) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnManaSpend, null, Owner, ManaCost);
        ExtendUse();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnHeroPowerUse, null, Owner, this).Invoke();
    }

    public override void ExtendUse() {
        Debug.Log("Extend Use");
    }

}