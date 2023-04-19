using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {
    public List<Effect> BuffEffects;
    public int HealthChange;
    public int AttackChange;
    public int CostChange;
    public int Expire {
        get => _expire;
        set {
            if (value == 0) {
                RemoveBuff();
            }
            _expire = value;
        }
    }
    private int _expire;

    public Buff(int HealthChange, int AttackChange) {
        this.HealthChange = HealthChange;
        this.AttackChange = AttackChange;
    }
    public Buff(Effect effect) {
        BuffEffects = new() {
            effect
        };
    }

    public void RemoveBuff() {
        BuffEffects.Clear();
        HealthChange = 0;
        AttackChange = 0;
    }
}
