using System;
using System.Collections.Generic;

public class ArgentProtector : MinionCard, ITarget, IBattlecryCard {
    public ICharacter Target { get; set; }
    public List<Effect> BattleCryEffects { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && (c as MinionLogic).Owner == Owner;

    private readonly Buff buff;

    public ArgentProtector(CardAsset CA) : base(CA) {
        buff = new(
            "ArgentProtector",
            null,
            null,
            new() { CharacterAttribute.DivineShield }
        );
    }

    public void BattleCry() {
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}