using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Effect {
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
        return Activator.CreateInstance(Type.GetType(Card.name.Replace(" ", "")), parameters) as CardBase;
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

}
