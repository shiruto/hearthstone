using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDieMessage : Message {
    private PlayerLogic p;
    private int DeadCreatureID;

    public MinionDieMessage(int CreatureID, PlayerLogic p) {
        this.p = p;
        this.DeadCreatureID = CreatureID;
    }

    public override void DealMessage() {
        // TODO tell view what to do
    }
}
