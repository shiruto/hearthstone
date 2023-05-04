using System.Collections.Generic;

public class MasterSwordsmith : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public MasterSwordsmith(CardAsset CA) : base(CA) {
        buff = new(
            "Master Swordsmith",
            new() { new(Status.Attack, Operator.Plus, 1) }
        );
        TriggersToGrant = new() { new(TurnEvent.OnTurnEnd, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Minion.Owner) return;
        Effect.GiveBuffEffect(buff, Effect.GetRandomObject(BattleControl.GetAllMinions(), (MinionLogic m) => Logic.IsEnemy(m, Minion) || m == Minion), Minion);
    }

}