using UnityEngine;

public class ExplosiveTrap : SecretCard {

    public ExplosiveTrap(CardAsset CA) : base(CA) {

    }

    public override void AddTrigger() {
        EventManager.AddListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void DelTrigger() {
        EventManager.DelListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void SecretImplement(BaseEventArgs e, out bool isTriggered) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target == Owner) {
            isTriggered = true;
            new DealAoeDamage(2, (ICharacter a) => {
                if (a is MinionLogic) {
                    return (a as MinionLogic).Owner != Owner;
                }
                else if (a is PlayerLogic) {
                    return (a as PlayerLogic) != Owner;
                }
                else return false;
            }, true).ActivateEffect();
        }
        else isTriggered = false;
    }

}