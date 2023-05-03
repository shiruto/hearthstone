using System.Collections.Generic;
using System.Linq;

public class Conceal : SpellCard {
    private readonly Buff buff;
    private List<MinionLogic> AffectedMinions;

    public Conceal(CardAsset CA) : base(CA) {
        buff = new(
            "Conceal",
            null,
            new() { new(TurnEvent.OnTurnStart, Triggered) },
            new() { CharacterAttribute.Stealth }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        AffectedMinions = BattleControl.GetAllMinions().Where((MinionLogic m) => Logic.IsEnemy(Owner, m)).ToList();
        foreach (MinionLogic m in AffectedMinions) {
            new GiveBuff(buff, this, m).ActivateEffect();
        }
    }

    private void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        foreach (MinionLogic m in AffectedMinions) {
            if (!m.isAlive) continue;
            (m as IBuffable).RemoveBuff(buff);
        }
    }

}