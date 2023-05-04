using System;

public class Eviscerate : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;
    public int Damage => 2;
    public int ComboDamage => 4;
    public override bool IsTriggered => BattleControl.IfUsedThisTurn();

    public Eviscerate(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (BattleControl.IfUsedThisTurn()) new DealDamageToTarget(ComboDamage, this, Target, false, true).ActivateEffect();
        else new DealDamageToTarget(Damage, this, Target, false, true).ActivateEffect();
    }

}