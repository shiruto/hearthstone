using System.Collections.Generic;
using UnityEngine;

public class TotemicCall : SkillCard {
    private readonly List<CardAsset> pool;
    private List<CardAsset> CurPool;
    public override bool CanBePlayed {
        get {
            UpdatePool();
            return base.CanBePlayed && CurPool.Count != 0;
        }
    }

    public TotemicCall(CardAsset CA) : base(CA) {
        string path = "ScriptableObject/UnCollectableCard/";
        pool = new() {
            Resources.Load<CardAsset>(path + "Searing Totem.asset"),
            Resources.Load<CardAsset>(path + "Healing Totem.asset"),
            Resources.Load<CardAsset>(path + "Stoneclaw Totem.asset"),
            Resources.Load<CardAsset>(path + "Strength Totem.asset")
        };
    }

    public override void ExtendUse() {
        base.ExtendUse();
        MinionCard Card = Effect.GetRandomCard(pool) as MinionCard;
        Owner.Field.SummonMinionAt(-1, new(Effect.GetRandomObject(CurPool)));
    }

    private bool UpdatePool() {
        CurPool = new(pool);
        foreach (MinionLogic m in Owner.Field.GetMinions()) {
            CurPool.Remove(m.Card.CA);
        }
        return CurPool.Count != 0;
    }

}