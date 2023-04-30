using System.Collections.Generic;

public class VooDooDoctor : MinionCard, IHeal, IBattleCry, ITarget {
    public int Heal { get => 2; }
    public List<Effect> BattleCryEffects { get; set; }
    public ICharacter Target { get; set; }

    public VooDooDoctor(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        new DealDamageToTarget(true, Heal, this, Target);
    }

    public bool CanBeTarget(ICharacter Card) {
        return true;
    }

}