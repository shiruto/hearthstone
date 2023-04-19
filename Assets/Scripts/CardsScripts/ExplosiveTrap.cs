using System;

public class ExplosiveTrap : SecretCard {
    public ExplosiveTrap(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.BeforeMinonAttack, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.owner != owner && evt.Sender.GetComponent<DraggableMinion>()._target == owner) {
            new DealAoeDamage(2, (ICharacter taget) => {
                if (Target is MinionLogic && (Target as MinionLogic).owner != owner) {
                    return true;
                }
                else if (Target is PlayerLogic && (Target as PlayerLogic) != owner) {
                    return true;
                }
                else return false;
            }).ActivateEffect();
        }

    }
}