using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {
    public MinionLogic Target;
    public Effect BuffEffect;
    public int HealthChange;
    public int AttackChange;
    public int CostChange;
    public int Expire {
        get => _expire;
        set => _expire = value;
    }
    private int _expire;
    public Buff(int HealthChange, int AttackChange) {
        this.HealthChange = HealthChange;
        this.AttackChange = AttackChange;
    }
    public Buff(Effect effect) {
        BuffEffect = effect;
    }

    public void RemoveBuff() {
        BuffEffect = null;
        HealthChange = 0;
        AttackChange = 0;
    }
}
