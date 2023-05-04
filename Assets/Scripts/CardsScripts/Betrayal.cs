using System;

public class Betrayal : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && Logic.IsEnemy(c, Owner);

    public Betrayal(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        int pos = BattleControl.GetEnemy(Owner).Field.GetPosition(Target as MinionLogic);
        (BattleControl.GetEnemy(Owner).Field.MinionAt(pos - 1) as ITakeDamage)?.TakeDamage(Target.Attack, Target);
        (BattleControl.GetEnemy(Owner).Field.MinionAt(pos + 1) as ITakeDamage)?.TakeDamage(Target.Attack, Target);
    }

}