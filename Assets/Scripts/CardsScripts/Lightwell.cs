using System.Collections.Generic;

public class Lightwell : MinionCard, ITriggerMinionCard, IHeal {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    public int Heal => 3;

    public Lightwell(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(TurnEvent.OnTurnStart, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        Effect.GetRandomObject(BattleControl.GetAllCharacters(), (ICharacter c) => Logic.IsEnemy(c, Owner) || (c.Health == c.MaxHealth)).Healing(Heal, Minion);
    }

}