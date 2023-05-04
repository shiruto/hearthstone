using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Effect {
    public virtual string Name => "empty effect";
    public EffectType effectType;

    public virtual void ActivateEffect() { // TODO: remove the activate?
        Debug.Log("No Card Effect");
    }

    public static CardBase GetRandomCard(List<CardBase> pool) {
        CardBase Card = pool[Random.Range(0, pool.Count)];
        return Card;
    }

    public static CardBase GetRandomCard(List<CardAsset> pool) {
        CardAsset Card = pool[Random.Range(0, pool.Count)];
        object[] parameters = new object[] { Card };
        return Activator.CreateInstance(Type.GetType(Card.name.FormatString()), parameters) as CardBase;
    }

    public static T GetRandomObject<T>(List<T> pool, Predicate<T> matchToDel = null) {
        matchToDel ??= (T a) => false;
        pool.RemoveAll(matchToDel);
        if (pool.Count == 0) {
            throw new InvalidOperationException("The pool is empty.");
        }
        T Card = pool[Random.Range(0, pool.Count)];
        return Card;
    }

    public static List<T> GetMultiRandomObject<T>(List<T> pool, int Num, Predicate<T> matchToDel = null) {
        matchToDel ??= (T a) => false;
        List<T> Objects = new();
        pool.RemoveAll(matchToDel);
        for (int i = 0; i < Num; i++) {
            if (pool.Count == 0) break;
            T t = pool[Random.Range(0, pool.Count)];
            pool.Remove(t);
            Objects.Add(t);
        }
        return Objects;
    }

    public static void GetArmorEffect(int value, PlayerLogic Player, IBuffable source) {
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.BeforeGetArmor, Player, source, ref value).Invoke();
        Player.GetArmor(value);
        EventManager.Allocate<DamageEventArgs>().CreateEventArgs(DamageEvent.GetArmor, Player, source, ref value).Invoke();
    }

    public static void DrawCardEffect(int value, PlayerLogic Player, IBuffable source) {
        Player.Deck.DrawCards(value);
    }

    public static void GiveBuffEffect(Buff buff, IBuffable taker, IBuffable giver) {
        taker?.AddBuff(buff);
    }

    public static void GiveAoeBuffEffect(Buff buff, List<IBuffable> pool, Predicate<IBuffable> match, IBuffable giver) {
        List<IBuffable> BuffTarget = pool.FindAll(match);
        if (BuffTarget.Count == 0) return;
        foreach (IBuffable b in BuffTarget) {
            b?.AddBuff(buff);
        }
    }

    public static void DestroyEffect(ITakeDamage taker, IBuffable giver) {
        taker.Die();
    }

    public static void DestroyAllEffect(IBuffable giver, Predicate<ITakeDamage> match) {
        foreach (ITakeDamage t in BattleControl.GetAllDestroyable().FindAll(match)) {
            t.Die();
        }
    }

    public static void DealDamage(int value, ITakeDamage taker, IBuffable giver, DamageType damageType = DamageType.None) {
        if (damageType == DamageType.Spell) value += BattleControl.Instance.SpellDamage;
        taker.TakeDamage(value, giver);
    }

}
