using UnityEngine;

public class ExplosiveTrap : SecretCard {

    public ExplosiveTrap(CardAsset CA) : base(CA) {
        trigger = new(AttackEvent.BeforeAttack, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target != Owner)
            return false;
        new DealAoeDamage(2, this, (ICharacter a) => {
            if (a is MinionLogic) {
                return (a as MinionLogic).Owner != Owner;
            }
            else if (a is PlayerLogic) {
                return (a as PlayerLogic) != Owner;
            }
            else return false;
        }, true).ActivateEffect();
        return true;
    }

}