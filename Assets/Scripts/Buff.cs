using System.Collections.Generic;
using UnityEngine;

public class Buff {
    public readonly string BuffName;
    public List<StatusChange> statusChange;
    public List<TriggerStruct> Triggers;
    public HashSet<CharacterAttribute> Attributes;

    public Buff(string name, List<StatusChange> sc, List<TriggerStruct> triggers = null, HashSet<CharacterAttribute> attributes = null) {
        BuffName = name;
        statusChange = sc;
        Triggers = triggers;
        Attributes = attributes;
    }

    public static void Modify(ref int variable, Operator op, int value) {
        switch (op) {
            case Operator.Plus:
                variable += value;
                break;
            case Operator.Minus:
                variable -= value;
                break;
            case Operator.Time:
                variable *= value;
                break;
            case Operator.Divide:
                variable /= value;
                break;
            case Operator.equal:
                variable = value;
                break;
            default:
                Debug.Log("no such operator");
                break;
        }
    }

}
