using System;
using System.Collections.Generic;

public class VooDooDoctor : MinionCard, IHeal, IBattlecryCard, ITarget {
    public int Heal => 2;
    public List<Effect> BattleCryEffects { get; set; }
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter a) => true;

    public VooDooDoctor(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        new DealDamageToTarget(Heal, this, Target, true).ActivateEffect();
    }

}