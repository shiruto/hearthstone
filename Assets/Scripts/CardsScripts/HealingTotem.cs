using System.Collections.Generic;

public class HealingTotem : MinionCard, ITriggerMinionCard, IHeal {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    public int Heal => 1;

    public HealingTotem(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(TurnEvent.OnTurnEnd, Triggered) };
    }


    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        new DealAoeDamage(Heal, Minion, (ICharacter c) => c is MinionLogic && (c as MinionLogic).Owner == Owner, true).ActivateEffect();
    }

}