public class GiveCard : Effect {

    readonly CardBase CardToGive;
    readonly int position;
    readonly PlayerLogic player;
    public override string Name => "Give Card Effect";

    public GiveCard(PlayerLogic player, int position, CardBase cardToGive) {
        this.player = player;
        this.position = position;
        CardToGive = cardToGive;
    }

    public override void ActivateEffect() {
        player.Hand.GetCard(position, CardToGive);
    }
}