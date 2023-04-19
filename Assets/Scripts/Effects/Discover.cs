using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Discover : Effect {
    public override string Name => "Discover Effect";
    private readonly List<CardBase> _opts;
    private readonly List<CardAsset> _pool1;
    private readonly List<CardBase> _pool2;
    private readonly bool type;

    public Discover(List<CardBase> _pool) {
        effectType = GameDataAsset.EffectType.Discover;
        _opts = new();
        _pool2 = new(_pool);
        type = true;
    }

    public Discover(List<CardAsset> _pool) {
        effectType = GameDataAsset.EffectType.Discover;
        _opts = new();
        _pool1 = new(_pool);
        type = false;
    }

    public override void ActivateEffect() {
        if (type) {
            for (int i = 0; i < 3; i++) {
                if (i < _pool2.Count) {
                    CardBase _cardToSelect = _pool2[Random.Range(0, _pool2.Count)];
                    _opts.Add(_cardToSelect);
                    _pool2.Remove(_cardToSelect);
                }
            }
        }
        else {
            for (int i = 0; i < 3; i++) {
                if (i < _pool1.Count) {
                    CardAsset _cardToSelect = _pool1[Random.Range(0, _pool1.Count)];
                    _opts.Add(Activator.CreateInstance(Type.GetType(_cardToSelect.name.Replace(" ", ""))) as CardBase);
                    _pool1.Remove(_cardToSelect);
                }
            }
        }
        BattleControl.Instance.ShowDiscoverPanel(_opts);
    }
}