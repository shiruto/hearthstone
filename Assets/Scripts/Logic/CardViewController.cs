using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewController : MonoBehaviour, IIdentifiable {

    private readonly int _cardID;
    public int ID {
        get => _cardID;
    }

    public PlayerLogic owner;
    public CardAsset CardData;

    public int orgManaCost;
    public int orgHealth;
    public int orgAttack;

    private int _manaCost;
    public int ManaCost {
        get => _manaCost;
        set {
            if (value < 0) {
                _manaCost = 0;
            }
            else _manaCost = value;
        }
    }
    private int _health;
    public int Health {
        get => _health;
        set {
            if (value < 0) {
                _health = 0;
            }
            else _health = value;
        }
    }
    private int _attack;
    public int Attack {
        get => _attack;
        set {
            if (value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    public List<Buff> Buffs {
        get => Buffs;
        set => Buffs = value;
    }

    private bool _canUse = true;
    public bool CanUse {
        get {
            return BattleControl.Instance.ActivePlayer == owner && ManaCost <= owner.Mana.Manas && _canUse;
        }
        set => _canUse = value;
    }


    public CardViewController(CardAsset CA) {
        CardData = CA;
        orgManaCost = CA.ManaCost;
        if (CA.cardType == GameDataAsset.CardType.Minion) {
            MinionCardAsset MCA = CA as MinionCardAsset;
            orgHealth = MCA.Health;
            orgAttack = MCA.Attack;
        }
        Buffs.Clear();
        _cardID = IDFactory.GetID();
    }

}
