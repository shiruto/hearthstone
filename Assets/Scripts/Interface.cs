using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public interface IDiscover { // TODO: could other special effect be interface too?
    public void DiscoverHandler(BaseEventArgs e);
    public List<CardBase> GetPool();
}

public interface ITarget { // with pointer
    public ICharacter Target { get; set; }
    public bool CanBeTarget(ICharacter Character);
}

public interface IHeal {
    public int Heal { get; }
}

public interface IDealDamage {
    public int Damage { get; }
}

public interface IGrantTrigger {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    public void Triggered(BaseEventArgs e);
}

public interface IBuffable {
    public List<Buff> BuffList { get; set; }
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
        if (buff.Triggers?.Count != 0) {
            foreach (TriggerStruct t in buff.Triggers) {
                AddTrigger(t);
            }
        }
        BuffList.Add(buff);
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
    void Die();
}

public interface IAttribute {
    HashSet<CharacterAttribute> Attributes { get; set; }
}

public interface IIdentifiable {
    int ID { get; }
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
