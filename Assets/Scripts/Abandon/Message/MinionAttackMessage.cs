using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackMessage : Message {
    private int TargetID;
    private int AttackerID;
    private int AttackerHealthAfter;
    private int TargetHealthAfter;
    private int DamageTakenByAttacker;
    private int DamageTakenByTarget;

    public MinionAttackMessage(int targetID, int attackerID, int damageTakenByAttacker, int damageTakenByTarget, int attackerHealthAfter, int targetHealthAfter) {
        TargetID = targetID;
        AttackerID = attackerID;
        AttackerHealthAfter = attackerHealthAfter;
        TargetHealthAfter = targetHealthAfter;
        DamageTakenByTarget = damageTakenByTarget;
        DamageTakenByAttacker = damageTakenByAttacker;
    }
}
