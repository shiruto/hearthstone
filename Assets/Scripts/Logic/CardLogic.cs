using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : IIdentifiable {
    public int ID {
        get => CardID;
    }
    private int CardID;
    public PlayerLogic owner;
    public CardAsset CardAssetInLogic;
    private readonly int baseManaCost;
    private int _manaCost;
    public int ManaCost {
        get => _manaCost;
        set {
            if (value < 0) {
                _manaCost = 0;
            }
            _manaCost = value;
        }
    }
    public List<Buff> Buffs {
        get => Buffs;
        set => Buffs = value;
    }
    public List<Effect> effects;
    public bool canUse = true;
    public bool CanBePlayed => BattleControl.Instance.ActivePlayer == owner && (_manaCost >= owner.Mana.Manas) && canUse;
    public int Health;
    public int Attack {
        get => Attack;
        set {
            if (value < 0) {
                Attack = 0;
            }
            else Attack = value;
        }
    }
    public CardLogic(CardAsset CA) {
        baseManaCost = CA.ManaCost;
        _manaCost = baseManaCost;
        Buffs.Clear();
        effects.Clear();
        CardID = IDFactory.GetID();
        if (CA.SpellScriptName != null && CA.SpellScriptName != "") {
            effects.Add(System.Activator.CreateInstance(System.Type.GetType(CA.SpellScriptName)) as Effect);
        }
    }
}
