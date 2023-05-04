using System;
using System.Collections.Generic;

public class Thoughtsteal : SpellCard {

    public Thoughtsteal(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        List<CardBase> Cards = Effect.GetMultiRandomObject(BattleControl.GetEnemy(Owner).Deck.Deck, 2);
        if (Cards.Count == 0) return;
        foreach (CardBase c in Cards) {
            Owner.Hand.GetCard(-1, Activator.CreateInstance(Type.GetType(c.CA.name.Replace(" ", "")), new object[] { c.CA }) as CardBase);
        }
    }

}