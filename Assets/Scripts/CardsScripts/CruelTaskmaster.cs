using System;

public class CruelTaskmaster : MinionCard, IBattlecryCard, IDealDamage, ITarget {
    public int Damage => 1;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    private readonly Buff buff;

    public CruelTaskmaster(CardAsset CA) : base(CA) {
        buff = new(
            "Cruel Taskmaster",
            new() { new(Status.Attack, Operator.Plus, 2) }
        );
    }

    public void BattleCry() {
        new GiveBuff(buff, Minion, Target).ActivateEffect();
        new DealDamageToTarget(Damage, Minion, Target).ActivateEffect();
    }

}