using System;

public class CrazedAlchemist : MinionCard, IBattlecryCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    private Buff buff;

    public CrazedAlchemist(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        int health = Target.MaxHealth;
        buff = new(
            "Crazed Alchemist",
            new() { new(Status.Health, Operator.equal, Target.Attack), new(Status.Attack, Operator.equal, health) }
        );
        Effect.GiveBuffEffect(buff, Target, Minion);
    }

}