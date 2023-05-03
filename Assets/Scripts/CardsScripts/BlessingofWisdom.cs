using System;

public class BlessingofWisdom : SpellCard, ITarget {
    private readonly Buff buff;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;

    public BlessingofWisdom(CardAsset CA) : base(CA) {
        buff = new(
            "Blessing of Wisdom",
            null,
            new() { new(AttackEvent.BeforeAttack, Triggered) }
        );
    }

    private void Triggered(BaseEventArgs e) {
        Owner.Deck.DrawCards(1);
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}