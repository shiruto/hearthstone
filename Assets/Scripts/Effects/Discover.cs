using System.Collections.Generic;
using UnityEngine;

public class Discover : Effect {
    public override string Name => "Discover Effect";
    private readonly List<CardBase> _opts;
    private List<CardBase> _pool;
    readonly IDiscover card;

    public Discover(IDiscover disCard) {
        effectType = EffectType.Discover;
        _opts = new();
        card = disCard;
    }

    public override void ActivateEffect() {
        _pool = new(card.GetPool());
        EventManager.AddListener(CardEvent.OnDiscover, card.DiscoverHandler);
        int cardNum = _pool.Count;
        for (int i = 0; i < 3; i++) {
            if (i < cardNum) {
                CardBase _cardToSelect = _pool[Random.Range(0, _pool.Count)];
                _opts.Add(_cardToSelect);
                _pool.Remove(_cardToSelect);
            }
        }
        BattleControl.Instance.ShowDiscoverPanel(_opts);
    }

}