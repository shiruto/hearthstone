using System.Collections.Generic;
using UnityEngine;

public class SkillCard : CardBase {

    public List<Effect> effects;

    public SkillCard(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        Debug.Log("Use Hero Power");
        foreach (Effect effect in effects) {
            effect.ActivateEffect();
        }
        EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnHeroPowerUse, null, BattleControl.Instance.ActivePlayer, this));
    }

}