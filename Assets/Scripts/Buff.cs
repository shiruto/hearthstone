using System.Collections.Generic;

public class Buff {
    public string BuffName;
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

    public Buff(int HealthChange, int AttackChange, int CostChange = 0) {
        this.HealthChange = HealthChange;
        this.AttackChange = AttackChange;
        this.CostChange = CostChange;
    }

    public void RemoveBuff() {
        HealthChange = 0;
        AttackChange = 0;
    }

}
