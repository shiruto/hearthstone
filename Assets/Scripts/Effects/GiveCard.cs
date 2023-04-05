public class GiveCard : Effect {
    CardViewController CardToGive;
    int position;
    PlayerLogic player;
    public override string Name => "Give Card Effect";
    public GiveCard(PlayerLogic player, int position, CardViewController cardToGive) {
        this.player = player;
        this.position = position;
        CardToGive = cardToGive;
    }
    public override void ActivateEffect() {
        player.Hand.GetCard(position, CardToGive);
    }
}