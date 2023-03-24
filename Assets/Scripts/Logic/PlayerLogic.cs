using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour, ICharacter {
    public bool isEnemy;
    public DeckLogic Deck;
    public HandLogic Hand;
    public ManaLogic Mana;
    public BattleField Field;
    private int playerID;

    private PlayerLogic(bool isEnemy, HandLogic Hand, DeckLogic Deck, ManaLogic Mana, BattleField Field) {
        this.isEnemy = isEnemy;
        this.Hand = Hand;
        this.Deck = Deck;
        this.Mana = Mana;
        this.Field = Field;
        playerID = IDFactory.GetID();
    }

    private static PlayerLogic you = null;
    private static PlayerLogic opponent = null;

    public int Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int ID { get => playerID; }

    public static PlayerLogic You() {
        if (you == null) {
            you = new(false, HandLogic.YourHand(), DeckLogic.YourDeck(), ManaLogic.YourMana(), BattleField.YourField());
        }
        return you;
    }
    public static PlayerLogic Opponent() {
        if (opponent == null) {
            opponent = new(true, HandLogic.OpponentHand(), DeckLogic.OpponentDeck(), ManaLogic.OpponentMana(), BattleField.OpponentField());
        }
        return opponent;
    }

    public void Die() {

    }
}
