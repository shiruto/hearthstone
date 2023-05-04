using System.Collections.Generic;

public class ManaWraith : MinionCard, IAuraMinionCard {
    public List<AuraManager> AuraToGrant { get; }
    private readonly Buff buff;

    public ManaWraith(CardAsset CA) : base(CA) {
        buff = new(
            "Mana Wraith",
            new() { new(Status.ManaCost, Operator.Plus, 1) }
        );
        AuraToGrant = new() { new(buff, (IBuffable b) => b is MinionCard) };
    }

}