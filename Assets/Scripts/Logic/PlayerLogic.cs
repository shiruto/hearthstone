using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour, ICharacter {
    public bool isEnemy;
    public DeckLogic Deck;
    public HandLogic Hand;
    public ManaLogic Mana;
    public BattleField Field;
    public CardLogic Weapon;
    private int playerID;
    private int _health;
    private int MaxHealth;
    private bool _isStealth;
    public bool IsStealth { get => _isStealth; set => _isStealth = value; }
    private bool _isImmune;
    public bool IsImmune { get => _isImmune; set => _isImmune = value; }
    private bool _isLifeSteal;
    public bool IsLifeSteal { get => _isLifeSteal; set => _isLifeSteal = value; }
    private bool _isWindFury;
    public bool IsWindFury { get => _isWindFury; set => _isWindFury = value; }
    public int Health {
        get => _health;
        set {
            if (_health + value > MaxHealth) {
                _health = MaxHealth;
            }
            else _health += value;
        }
    }
    public int Attack {
        get => _attack;
        set {
            if (_attack + value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int _attack;
    public bool isStealth;

    public int ID { get => playerID; }
    public List<Buff> Buffs {
        get => Buffs;
        set => Buffs = value;
    }

    private void Awake() {
        // Draggable.OnCardUse += OnCardUseHandler;

        // Draggable.OnTargetedCardUse += OnTargetedCardUseHander;
    }

    // private void OnCardUseHandler(CardLogic CL) {
    //     if (CL.CardAssetInLogic.MaxHealth > 0) {
    //         UseMinionCard(CL, 0);
    //     }
    //     else {
    //         UseSpellCard(CL, null);
    //     }
    // }

    // private void OnTargetedCardUseHander(CardLogic CL, Transform Target) {
    //     if (Target.GetComponent<MinionManager>()) {
    //         UseSpellCard(CL, Target.GetComponent<MinionManager>());
    //     }
    //     else if (Target.GetComponent<PlayerLogic>()) {
    //         UseSpellCard(CL, Target.GetComponent<PlayerLogic>());
    //     }
    //     else {
    //         Debug.Log("Target Error");
    //     }
    // }

    // public void UseMinionCard(CardBase playedCard, int tablePos) {
    //     Mana.Manas -= playedCard.Card.CurManaCost;
    //     Field.SummonMinionAt(tablePos, new((MinionCard)playedCard.Card));
    //     // TODO new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
    //     Hand.DiscardCrad(playedCard);
    //     // TODO HighlightPlayableCards();
    // }

    // public void UseSpellCard(int SpellCardID, int TargetID) {
    //     // TODO: !!!
    //     // if TargetUnique ID < 0 , for example = -1, there is no target.
    //     if (TargetID < 0)
    //         UseSpellCard(BattleControl.CardCreated[SpellCardID], null);
    //     else if (TargetID == BattleControl.you.ID) {
    //         UseSpellCard(BattleControl.CardCreated[SpellCardID], BattleControl.you);
    //     }
    //     else if (TargetID == BattleControl.opponent.ID) {
    //         UseSpellCard(BattleControl.CardCreated[SpellCardID], BattleControl.opponent);
    //     }
    //     else {
    //         // target is a creature
    //         UseSpellCard(BattleControl.CardCreated[SpellCardID], BattleControl.MinionCreated[TargetID]);
    //     }

    // }
    // public void UseSpellCard(CardLogic playedCard, ICharacter target) {
    //     Mana.Manas -= playedCard.ManaCost;
    //     // cause effect instantly:
    //     // if (playedCard.effect != null)
    //     //     playedCard.effect.ActivateEffect(playedCard.CardAssetInLogic.SpellDamage, target);
    //     // else {
    //     //     Debug.LogWarning("No effect found on card " + playedCard.CardAssetInLogic.name);
    //     // }
    //     // new PlayASpellCardCommand(this, playedCard).AddToQueue();
    //     Hand.DiscardCrad(playedCard);
    // }

    public void DrawCard() {
        Hand.GetCard(-1, Deck.RemoveCard(0));
    }

    public void Die() {

    }
}
