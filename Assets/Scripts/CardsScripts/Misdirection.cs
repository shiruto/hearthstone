using System.Collections.Generic;
using UnityEngine;

public class Misdirection : SecretCard {
    public Misdirection(CardAsset CA) : base(CA) {

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
            List<ICharacter> AvailableTargets = new();
            AvailableTargets.AddRange(BattleControl.GetAllMinions());
            AvailableTargets.Add(BattleControl.opponent);
            ScnBattleUI.Instance.Targeting = AvailableTargets[Random.Range(0, AvailableTargets.Count)];
        }
        else isTriggered = false;
    }

}