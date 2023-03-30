using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaChange : Effect {
    public int Manas;
    public PlayerLogic owner = BattleControl.you;
    public override string Name => "mana change effect";
    public ManaChange(int Manas) {
        this.Manas = Manas;
    }
    public ManaChange(PlayerLogic owner, int Manas) {
        this.owner = owner;
        this.Manas = Manas;
    }
    public override void ActivateEffect() {
        owner.Mana.Manas += Manas;
    }
}
