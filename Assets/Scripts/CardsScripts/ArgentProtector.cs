using System.Collections.Generic;

public class ArgentProtector : MinionCard, ITarget, IBattleCry {
    public ICharacter Target { get; set; }
    public List<Effect> BattleCryEffects { get; set; }
    private readonly Buff buff;

    public ArgentProtector(CardAsset CA) : base(CA) {
        buff = new(
            "ArgentProtector",
            null,
            null,
            new() { CharacterAttribute.DivineShield }
        );
    }

    public bool CanBeTarget(ICharacter Character) {
        return Character is MinionLogic && (Character as MinionLogic).Owner == Owner;
    }

    public void BattleCry() {
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}