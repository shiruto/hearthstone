using System;

public class Naturalize : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;

    public Naturalize(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Target.Die();
        BattleControl.GetEnemy(Owner).Deck.DrawCards(2);
    }

}