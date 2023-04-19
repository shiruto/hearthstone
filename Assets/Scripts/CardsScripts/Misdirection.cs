using System.Collections.Generic;
using UnityEngine;

public class Misdirection : SpellCard {
    public Misdirection(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        BattleControl.you.Secrets.Add(this);
        EventManager.AddListener(MinionEvent.BeforeMinonAttack, Triggered);
    }

    public void Triggered(BaseEventArgs e) { // TODO: available target?
        GameObject Attacker = e.Sender;
        List<ICharacter> AvailableTargets = new();
        AvailableTargets.AddRange(BattleControl.you.Field.GetMinions());
        AvailableTargets.AddRange(BattleControl.opponent.Field.GetMinions());
        AvailableTargets.Add(BattleControl.opponent);
        Attacker.GetComponent<DraggableMinion>()._target = AvailableTargets[Random.Range(0, AvailableTargets.Count)];
    }
}