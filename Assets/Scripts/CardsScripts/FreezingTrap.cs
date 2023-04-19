using System;

public class FreezingTrap : SecretCard {
    public FreezingTrap(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.BeforeMinonAttack, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        // TODO: minion back to hand as a card
    }
}