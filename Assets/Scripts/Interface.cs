using System;
using System.Collections.Generic;

public interface IDiscover { // TODO: could other special effect be interface too?
    public void DiscoverHandler(BaseEventArgs e);
    public List<CardBase> GetPool();
}

public interface ITarget { // with pointer
    public ICharacter Target { get; set; }
    public bool CanBeTarget(CardBase Card);
}

public interface IHeal {
    public int Heal { get; }
}

public interface IDealDamage {
    public int Damage { get; }
}

public interface ITriggerMinionCard {
    public List<TriggerStruct> Triggers { get; set; }
    public void Triggered(BaseEventArgs e);
}

public interface IBuffable {
    public List<Buff> BuffList { get; set; }
}

public interface ICharacter : IIdentifiable, ITakeDamage, IBuffable {
    int Attack { get; set; }
    bool IsStealth { get; set; }
    bool IsImmune { get; set; }
    bool IsLifeSteal { get; set; }
    bool IsWindFury { get; set; }
    bool IsFrozen { get; set; }
    void Die();
}

public interface IIdentifiable {
    int ID { get; }
    public List<Buff> BuffList { get; set; }
}

public interface ITakeDamage {
    int Health { get; set; }
}

public interface IBattleCry {
    public List<Effect> BattleCryEffects { get; set; }
    public void BattleCry();
}

public interface IDeathRattle {
    public List<Effect> DeathRattleEffects { get; set; }
    public void DeathRattle();
}
