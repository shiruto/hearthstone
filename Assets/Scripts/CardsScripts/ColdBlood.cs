using System;

public class ColdBlood : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    private readonly Buff buff;
    private readonly Buff ComboBuff;

    public ColdBlood(CardAsset CA) : base(CA) {
        buff = new(
            "Cold Blood",
            new() { new(Status.Attack, Operator.Plus, 2) }
        );
        ComboBuff = new(
            "Cold Blood",
            new() { new(Status.Attack, Operator.Plus, 4) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (BattleControl.IfUsedThisTurn()) {
            new GiveBuff(ComboBuff, this, Target).ActivateEffect();
        }
        else new GiveBuff(buff, this, Target).ActivateEffect();
    }

}