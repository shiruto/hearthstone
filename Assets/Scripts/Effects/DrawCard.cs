public class DrawCard : Effect {
    public override string Name => "Draw Card Effect";
    public int CardNum;
    public PlayerLogic Owner;

    public DrawCard(PlayerLogic _owner, int _cardNum) {
        CardNum = _cardNum;
        Owner = _owner;
    }

    public override void ActivateEffect() {
        Owner.Deck.DrawCards(CardNum);
    }

}