using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Discover : Effect {
    public override string Name => "Discover Effect";
    private readonly List<CardBase> _opts;

    public Discover(List<CardBase> _pool) {
        effectType = GameDataAsset.EffectType.Discover;
        _opts = new();
        for (int i = 0; i < 3; i++) {
            CardBase _cardToSelect = _pool[Random.Range(0, _pool.Count)];
            _opts.Add(_cardToSelect);
            _pool.Remove(_cardToSelect);
        }
    }

    public Discover(List<CardAsset> _pool) {
        effectType = GameDataAsset.EffectType.Discover;
        _opts = new();
        for (int i = 0; i < 3; i++) {
            CardAsset _cardToSelect = _pool[Random.Range(0, _pool.Count)];
            _opts.Add(Activator.CreateInstance(Type.GetType(_cardToSelect.name.Trim())) as CardBase);
            _pool.Remove(_cardToSelect);
        }
    }

    public override void ActivateEffect() {
        BattleControl.Instance.ShowDiscoverPanel(_opts);
    }
}