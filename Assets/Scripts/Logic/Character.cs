using System.Collections.Generic;

public interface ICharacter : IIdentifiable {
    int Health { get; set; }
    int Attack { get; set; }
    bool IsStealth { get; set; }
    bool IsImmune { get; set; }
    bool IsLifeSteal { get; set; }
    bool IsWindFury { get; set; }
    bool IsFrozen { get; set; }
    bool CanAttack { get; set; }
    void Die();

    public void AttackAgainst(ICharacter target);
}

public interface IIdentifiable {
    int ID { get; }
    public List<Buff> Buffs { get; set; }
}
