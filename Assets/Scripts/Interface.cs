using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDiscover {
    public void DiscoverHandler(BaseEventArgs e);
    public List<CardBase> Pool { get; }
}

public interface ITarget { // with pointer
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match { get; }
}

public interface IHeal {
    public int Heal { get; }
}

public interface IDealDamage {
    public int Damage { get; }
}

public interface ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    public void Triggered(BaseEventArgs e);
}

public interface IBuffable {
    public List<Buff> BuffList { get; set; }
    public List<Buff> Auras { get; set; }
    public List<TriggerStruct> Triggers { get; set; }

    public void AddTrigger(TriggerStruct t) {
        Triggers.Add(t);
        EventManager.AddListener(t.eventType, t.callback);
    }

    public void RemoveTrigger(TriggerStruct t) {
        Triggers.Remove(t);
        EventManager.DelListener(t.eventType, t.callback);
    }

    void RemoveAllTriggers() {
        if (Triggers.Count == 0) return;
        foreach (var t in Triggers) {
            EventManager.DelListener(t.eventType, t.callback);
        }
    }

    public void AddBuff(Buff buff) {
        if (buff.Triggers != null && buff.Triggers.Count != 0) {
            foreach (TriggerStruct t in buff.Triggers) {
                AddTrigger(t);
            }
        }
        BuffList.Add(buff);
        ReadBuff();
    }

    public void AddAura(Buff buff) {
        if (buff.Triggers != null) {
            foreach (TriggerStruct t in buff.Triggers) {
                AddTrigger(t);
            }
        }
        Auras.Add(buff);
        Debug.Log("Add Aura");
        ReadBuff();
    }

    public void RemoveBuff(Buff buff) {
        if (buff.Triggers != null) {
            foreach (TriggerStruct t in buff.Triggers) {
                RemoveTrigger(t);
            }
        }
        BuffList.Remove(buff);
        ReadBuff();
    }

    public void RemoveAura(Buff buff) {
        if (buff.Triggers != null) {
            foreach (TriggerStruct t in buff.Triggers) {
                RemoveTrigger(t);
            }
        }
        Auras.Remove(buff);
        ReadBuff();
    }

    void RemoveAllBuff() {
        foreach (Buff b in BuffList) {
            if (b.Triggers.Count > 0) {
                foreach (TriggerStruct t in b.Triggers) {
                    RemoveTrigger(t);
                }
            }
        }
        BuffList.Clear();
        ReadBuff();
    }

    public void ReadBuff();

}

public interface ICharacter : IIdentifiable, ITakeDamage, IBuffable, IAttribute {
    int Attack { get; set; }
    int SpellDamage { get; set; }
    ICharacter AttackTarget { get; set; }

    void AttackAgainst(ICharacter target);

    public void RemoveAttribute(CharacterAttribute a) {
        Attributes.Remove(a);
        if (BuffList == null) return;
        foreach (var b in BuffList) {
            b.Attributes.Remove(a);
        }
    }
}

public interface IAttribute {
    HashSet<CharacterAttribute> Attributes { get; set; }
}

public interface IIdentifiable {
    int ID { get; }
}

public interface ITakeDamage {
    int Health { get; set; }
    int MaxHealth { get; set; }
    List<Action<MinionLogic>> Deathrattle { get; set; }

    void Die();

    void TakeDamage(int value, IBuffable source) {
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.BeforeTakeDamage, this, source, ref value).Invoke();
        Health -= value;
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.TakeDamage, this, source, ref value).Invoke();
    }

    void Healing(int value, IBuffable source) {
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.BeforeHealing, this, source, ref value).Invoke();
        Health += value;
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.Healing, this, source, ref value).Invoke();
    }

}

public interface IBattlecryCard {
    public void BattleCry();
}

public interface IDeathrattleCard {
    void Deathrattle(MinionLogic caller);
}

public interface IAuraMinionCard {
    public List<AuraManager> AuraToGrant { get; }
}

