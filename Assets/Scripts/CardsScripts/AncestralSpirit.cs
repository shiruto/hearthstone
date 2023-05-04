using System;

public class AncestralSpirit : SpellCard, ITarget, IDeathrattleCard {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;

    public AncestralSpirit(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        (Target as MinionLogic).Deathrattle.Add(Deathrattle);
    }

    public void Deathrattle(MinionLogic caller) {
        new SummonMinion(caller.Card, caller.Owner, caller.Owner.Field.GetPosition(caller)).ActivateEffect();
    }

}