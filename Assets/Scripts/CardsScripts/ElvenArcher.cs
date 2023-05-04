using System;
using System.Collections.Generic;
using System.Linq;

public class ElvenArcher : MinionCard, IBattlecryCard, IDealDamage, ITarget {
    public int Damage => 1;
    public List<Effect> BattleCryEffects { get; set; }
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;

    public ElvenArcher(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        new DealDamageToTarget(Damage, this, ScnBattleUI.Instance.TargetCharacter).ActivateEffect();
    }

}